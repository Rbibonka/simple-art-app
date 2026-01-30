using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonView
{
    private RectTransform grp_Premium;
    private Image image;
    private RectTransform loader;

    public GalleryButtonView(
        RectTransform grp_Premium,
        Image image,
        RectTransform loader)
    {
        this.grp_Premium = grp_Premium;
        this.image = image;
        this.loader = loader;
    }

    public void EnablePremium()
    {
        grp_Premium.gameObject.SetActive(true);
    }

    public void DisablePremium()
    {
        grp_Premium.gameObject.SetActive(false);
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void ResetSprite()
    {
        image.sprite = null;
    }

    public void HideLoader()
    {
        loader.gameObject.SetActive(false);
    }

    public void ShowLoader()
    {
        loader.gameObject.SetActive(true);
    }

    public Tween StartLoadScreen(float duration = 1f)
    {
        return loader
            .DORotate(
                new Vector3(0f, 0f, -360f),
                duration,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.InOutSine)
            .SetLoops(-1);
    }
}