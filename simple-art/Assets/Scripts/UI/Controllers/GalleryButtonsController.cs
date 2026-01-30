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

    private BaseObjectPool<GalleryButtonController> galleryButtonPool;

    public async UniTask InitializeAsync()
    {
        items = new();
        imageLoader = new();

        galleryButtonPool = new(imageButtonPrefab, content);

        for (int i = 1; i < 66; i++)
        {
            var item = galleryButtonPool.GetFromPool();

            bool isPremium = false;

            if (i % 4 == 0)
            {
                isPremium = true;
            }

            item.Initialize(i, isPremium);
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

        imageButton.EnableLoading();

        imageLoader.Enqueue(imageButton.Index, sprite =>
        {
            if (sprite != null)
            {
                imageButton.SetSprite(sprite);
            }
        });
    }
}