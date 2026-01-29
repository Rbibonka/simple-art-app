using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private LazyImageLoader imageButtonPrefab;

    [SerializeField]
    private RectTransform content;

    private ScrollViewVisibleChecker scrollViewVisibleChecker;

    private ImageLoader imageLoader;

    private List<LazyImageLoader> items;

    private CancellationTokenSource cts;

    public void Start()
    {
        items = new();
        imageLoader = new();

        cts = new();

        for (int i = 1; i < 66; i++)
        {
            var item = Instantiate(imageButtonPrefab, content);
            item.Initialize(i);

            items.Add(item);
        }

        scrollViewVisibleChecker = new(scrollRect, items);
        scrollViewVisibleChecker.imageButtonIsVisible += ImageButtonIsVisible;
    }

    private void OnDestroy()
    {
        imageLoader.Dispose();
    }

    private void ImageButtonIsVisible(LazyImageLoader imageButton)
    {
        if (imageButton.IsLoaded)
        {
            return;
        }

        imageLoader.Enqueue(imageButton.ImageIndex, sprite =>
        {
            if (sprite != null)
            {
                imageButton.SetSprite(sprite);
            }
        });
    }
}