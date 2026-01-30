using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonsController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private GalleryButtonController imageButtonPrefab;

    [SerializeField]
    private RectTransform content;

    [SerializeField]
    private RectTransform emptyPoolParent;

    [SerializeField]
    private Sprite defaultSprite;

    private TabBarController tabBarController;
    private ImageButtonsCreator imageButtonsSetter;
    private ScrollViewController scrollViewController;
    private ImageLoader imageLoader;
    private List<GalleryButtonController> items;

    public async UniTask InitializeAsync(TabBarController tabBarController)
    {
        this.tabBarController = tabBarController;

        items = new();
        imageLoader = new();
        imageButtonsSetter = new(imageButtonPrefab, content, emptyPoolParent);

        items = imageButtonsSetter.CreateAllImageButtons();

        await UniTask.NextFrame();
        await UniTask.NextFrame();

        scrollViewController = new(scrollRect, scrollbar, items);
        scrollViewController.imageButtonIsVisible += ImageButtonIsVisible;
        scrollViewController.InitialCheck();

        this.tabBarController.ButtonOddClicked += ButtonOddClicked;
        this.tabBarController.ButtonAllClicked += ButtonAllClicked;
        this.tabBarController.ButtonEvenClicked += ButtonEvenClicked;

        imageButtonsSetter.ButtonClicked += OnButtonClicked;
    }

    private void OnButtonClicked(bool isPremium)
    {
        if (isPremium)
        {
            Debug.Log("Pre");
        }
        else
        {
            Debug.Log("Ne Pre");
        }
    }

    public void Deinitialize()
    {
        imageLoader.Dispose();
    }

    private void ButtonEvenClicked()
    {
        imageLoader.Restart();
        imageButtonsSetter.CreateEvenImageButtons();
        scrollViewController.ResetScroll();
    }

    private void ButtonAllClicked()
    {
        imageLoader.Restart();
        imageButtonsSetter.CreateAllImageButtons();
        scrollViewController.ResetScroll();
    }

    private void ButtonOddClicked()
    {
        imageLoader.Restart();
        imageButtonsSetter.CreateOddImageButtons();
        scrollViewController.ResetScroll();
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
            else
            {
                imageButton.SetSprite(defaultSprite);
            }
        });
    }
}