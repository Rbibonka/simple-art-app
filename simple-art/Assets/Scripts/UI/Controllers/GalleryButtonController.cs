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

    private GalleryButtonModel galleryButtonModel;
    private GalleryButtonView galleryButtonView;

    public void Initialize(int index, bool isPremium)
    {
        galleryButtonModel = new(index, isPremium);
        galleryButtonView = new(grp_Premium, img_Content);

        SetType();
    }

    public void EnableLoading()
    {
        galleryButtonModel.SetLoading(true);
    }

    public void SetSprite(Sprite sprite)
    {
        galleryButtonView.SetSprite(sprite);
    }

    public void ResetSprite()
    {
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