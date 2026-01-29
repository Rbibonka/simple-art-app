using UnityEngine;
using UnityEngine.UI;

public class CarouselDotsView
{
    private Sprite activeSprite;
    private Sprite inactiveSprite;

    private Image[] dots;

    public CarouselDotsView(
        Sprite activeSprite,
        Sprite inactiveSprite,
        Image[] dots)
    {
        this.activeSprite = activeSprite;
        this.inactiveSprite = inactiveSprite;
        this.dots = dots;
    }

    public void ValidateCount(int slidesCount)
    {
        if (dots.Length != slidesCount)
        {
            Debug.LogWarning(
                $"[CarouselDotsView] Dots count ({dots.Length}) " +
                $"does not match slides count ({slidesCount})"
            );
        }
    }

    public void SetActive(int index)
    {
        for (int i = 0; i < dots.Length; i++)
            dots[i].sprite = i == index ? activeSprite : inactiveSprite;
    }
}