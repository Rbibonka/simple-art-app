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

    private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    public ImageLoader()
    {
        cts = new();
    }

    private readonly Queue<Request> queue = new Queue<Request>();
    private bool isProcessing;

    private CancellationTokenSource cts;
    private bool disposed;

    public void Enqueue(int index, Action<Sprite> onCompleted)
    {
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

    private async UniTaskVoid ProcessQueueAsync(CancellationToken ct)
    {
        isProcessing = true;

        while (queue.Count > 0)
        {
            var request = queue.Dequeue();
            var sprite = await DownloadSpriteAsync(request.Url, ct).AttachExternalCancellation(ct);
            request.Callback?.Invoke(sprite);
        }

        isProcessing = false;
    }

    private async UniTask<Sprite> DownloadSpriteAsync(string url, CancellationToken ct)
    {
        using var uwr = UnityWebRequestTexture.GetTexture(url);

        try
        {
            await uwr.SendWebRequest().ToUniTask(cancellationToken: ct);

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
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        cts?.Cancel();
        cts?.Dispose();

        cts = null;
    }
}