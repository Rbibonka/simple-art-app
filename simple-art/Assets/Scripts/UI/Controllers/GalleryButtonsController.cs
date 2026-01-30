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

    private ImageButtonsCreator imageButtonsSetter;
    private ScrollViewVisibleChecker scrollViewVisibleChecker;
    private ImageLoader imageLoader;
    private List<GalleryButtonController> items;

    public async UniTask InitializeAsync()
    {
        items = new();
        imageLoader = new();
        imageButtonsSetter = new(imageButtonPrefab, content);

        items = imageButtonsSetter.CreateOddImageButtons();

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