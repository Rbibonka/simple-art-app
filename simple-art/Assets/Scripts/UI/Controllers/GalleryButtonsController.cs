using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonsController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private GalleryButtonController imageButtonPrefab;

    [SerializeField]
    private RectTransform content;

    private ScrollViewVisibleChecker scrollViewVisibleChecker;

    private ImageLoader imageLoader;

    private List<GalleryButtonController> items;

    public async UniTask InitializeAsync()
    {
        items = new();
        imageLoader = new();

        for (int i = 1; i < 66; i++)
        {
            var item = Instantiate(imageButtonPrefab, content);
            item.Initialize(i);

            items.Add(item);
        }

        await UniTask.NextFrame();
        await UniTask.NextFrame();

        scrollViewVisibleChecker = new(scrollRect, items);
        scrollViewVisibleChecker.imageButtonIsVisible += ImageButtonIsVisible;
        scrollViewVisibleChecker.InitialCheck();
    }

    public void Deinitialize()
    {
        imageLoader.Dispose();
    }

    private void ImageButtonIsVisible(GalleryButtonController imageButton)
    {
        if (imageButton.IsLoading)
        {
            return;
        }

        imageButton.StartLoading();

        imageLoader.Enqueue(imageButton.ImageIndex, sprite =>
        {
            if (sprite != null)
            {
                imageButton.SetSprite(sprite);
            }
        });
    }
}