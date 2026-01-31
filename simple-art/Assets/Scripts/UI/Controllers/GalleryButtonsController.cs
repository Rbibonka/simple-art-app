using Cysharp.Threading.Tasks;
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

    [SerializeField]
    private PopupController premiumPopupController;

    [SerializeField]
    private PopupController defaultPopupController;

    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;

    private TabBarController tabBarController;
    private ImageButtonsCreator imageButtonsSetter;
    private ScrollViewController scrollViewController;
    private ImageLoader imageLoader;
    private List<GalleryButtonController> items;

    public async UniTask InitializeAsync(TabBarController tabBarController, bool isTablet)
    {
        this.tabBarController = tabBarController;

        premiumPopupController.Initialize();
        premiumPopupController.buttonBackClicked += OnButtonBackClicked;

        defaultPopupController.Initialize();
        defaultPopupController.buttonBackClicked += OnButtonBackClicked;

        items = new();
        imageLoader = new();
        imageButtonsSetter = new(imageButtonPrefab, content, emptyPoolParent);

        items = imageButtonsSetter.CreateOddImageButtons();

        SetGalleryGrid(isTablet);

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

    private void OnButtonBackClicked()
    {
        premiumPopupController.Hide();
        defaultPopupController.Hide();
    }

    private void OnButtonClicked(GalleryButtonController button)
    {
        if (button.IsPremium)
        {
            premiumPopupController.Show();
        }
        else
        {
            if (button.Sprite == null)
            {
                defaultPopupController.SetupContent(defaultSprite);
            }
            else
            {
                defaultPopupController.SetupContent(button.Sprite);
            }

            defaultPopupController.Show();
        }
    }

    public void Deinitialize()
    {
        premiumPopupController.buttonBackClicked -= OnButtonBackClicked;
        scrollViewController.imageButtonIsVisible -= ImageButtonIsVisible;

        tabBarController.ButtonOddClicked -= ButtonOddClicked;
        tabBarController.ButtonAllClicked -= ButtonAllClicked;
        tabBarController.ButtonEvenClicked -= ButtonEvenClicked;

        imageButtonsSetter.ButtonClicked -= OnButtonClicked;

        defaultPopupController.buttonBackClicked -= OnButtonBackClicked;

        imageLoader.Dispose();

        premiumPopupController.Deinitialize();
        defaultPopupController.Deinitialize();
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

    private void SetGalleryGrid(bool isTablet)
    {
        if (isTablet)
        {
            gridLayoutGroup.cellSize = new Vector2(400, 400);
            gridLayoutGroup.constraintCount = 3;

            return;
        }

        gridLayoutGroup.cellSize = new Vector2(640, 640);
        gridLayoutGroup.constraintCount = 2;
    }
}