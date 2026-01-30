using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonController : PoolableObject
{
    public int Index => galleryButtonModel.Index;

    public bool IsLoading => galleryButtonModel.IsLoading;

    public bool IsPremium => galleryButtonModel.IsPremium;

    public Sprite Sprite => img_Content.sprite;

    [SerializeField]
    private Button btn_ImageButton;

    [SerializeField]
    private Image img_Content;

    [SerializeField]
    private RectTransform grp_Premium;

    [SerializeField]
    private RectTransform loader;

    private GalleryButtonModel galleryButtonModel;
    private GalleryButtonView galleryButtonView;

    public event Action<GalleryButtonController> ButtonClicked;

    private void Start()
    {
        galleryButtonView.StartLoadScreen();
    }

    public void Initialize(int index, bool isPremium)
    {
        galleryButtonModel = new(index, isPremium);
        galleryButtonView = new(grp_Premium, img_Content, loader);

        SetType();
        galleryButtonView.ShowLoader();

        btn_ImageButton.onClick.AddListener(OnButtonClicked);
    }

    public void Deinitialize()
    {
        btn_ImageButton.onClick.RemoveListener(OnButtonClicked);
    }

    public void EnableLoading()
    {
        galleryButtonModel.SetLoading(true);
    }

    public void SetSprite(Sprite sprite)
    {
        galleryButtonView.SetSprite(sprite);
        galleryButtonView.HideLoader();
    }

    public void ResetSprite()
    {
        galleryButtonView.ShowLoader();

        galleryButtonView.ResetSprite();
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke(this);
    }

    private void SetType()
    {
        if (galleryButtonModel.IsPremium)
        {
            galleryButtonView.EnablePremium();
        }
        else
        {
            galleryButtonView.DisablePremium();
        }
    }
}