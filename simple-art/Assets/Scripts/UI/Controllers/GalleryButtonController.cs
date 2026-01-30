using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonController : PoolableObject
{
    public int Index => galleryButtonModel.Index;

    public bool IsLoading => galleryButtonModel.IsLoading;

    [SerializeField]
    private Image img_Content;

    [SerializeField]
    private RectTransform grp_Premium;

    [SerializeField]
    private RectTransform loader;

    private GalleryButtonModel galleryButtonModel;
    private GalleryButtonView galleryButtonView;

    private Tween spinTween;

    public void Initialize(int index, bool isPremium)
    {
        galleryButtonModel = new(index, isPremium);
        galleryButtonView = new(grp_Premium, img_Content, loader);

        SetType();
        spinTween = galleryButtonView.StartLoadScreen();
    }

    public void EnableLoading()
    {
        galleryButtonModel.SetLoading(true);
    }

    public void SetSprite(Sprite sprite)
    {
        galleryButtonView.SetSprite(sprite);
        spinTween.Kill();
    }

    public void ResetSprite()
    {
        spinTween = galleryButtonView.StartLoadScreen();
        galleryButtonView.ResetSprite();
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