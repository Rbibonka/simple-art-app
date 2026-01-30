using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonView
{
    private RectTransform grp_Premium;
    private Image image;

    public GalleryButtonView(
        RectTransform grp_Premium,
        Image image)
    {
        this.grp_Premium = grp_Premium;
        this.image = image;
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
}