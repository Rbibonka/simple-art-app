using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader : IDisposable
{
    private class Request
    {
        public string Url;
        public Action<Sprite> Callback;
    }

    private readonly string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    private readonly Queue<Request> queue = new Queue<Request>();

    private CancellationTokenSource cts;
    private bool isProcessing;
    private bool disposed;

    public ImageLoader()
    {
        cts = new CancellationTokenSource();
    }

    public void Enqueue(int index, Action<Sprite> onCompleted)
    {
        if (disposed)
            return;

        queue.Enqueue(new Request
        {
            Url = baseUrl + index + ".jpg",
            Callback = onCompleted
        });

        if (!isProcessing)
        {
            _ = ProcessQueueAsync(cts.Token);
        }
    }

    public void StopAndClear()
    {
        if (disposed)
            return;

        cts.Cancel();
        cts.Dispose();

        cts = new CancellationTokenSource();

        queue.Clear();
        isProcessing = false;
    }

    public void Restart()
    {
        if (disposed)
            return;

        StopAndClear();

        if (queue.Count > 0 && !isProcessing)
        {
            _ = ProcessQueueAsync(cts.Token);
        }
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        cts.Cancel();
        cts.Dispose();
        cts = null;

        queue.Clear();
    }

    private async UniTaskVoid ProcessQueueAsync(CancellationToken ct)
    {
        isProcessing = true;

        try
        {
            while (queue.Count > 0)
            {
                ct.ThrowIfCancellationRequested();

                var request = queue.Dequeue();
                var sprite = await DownloadSpriteAsync(request.Url, ct);

                request.Callback?.Invoke(sprite);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Operation canceled");
        }
        finally
        {
            isProcessing = false;
        }
    }

    private async UniTask<Sprite> DownloadSpriteAsync(string url, CancellationToken ct)
    {
        using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutCts.Token);

        using var uwr = UnityWebRequestTexture.GetTexture(url);

        try
        {
            await uwr.SendWebRequest().ToUniTask(cancellationToken: linkedCts.Token);

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Image download failed: {url}\n{uwr.error}");
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(uwr);

            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
        catch (OperationCanceledException)
        {
            // Отличаем тайм-аут от внешней отмены при необходимости
            if (timeoutCts.IsCancellationRequested && !ct.IsCancellationRequested)
            {
                Debug.LogWarning($"Image download timeout (10s): {url}");
            }

            return null;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }
}