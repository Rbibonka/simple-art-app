using DG.Tweening;
using UnityEngine;

public class CarouselView
{
    private RectTransform content;
    private RectTransform[] panels;
    private float spacing = 0f;

    private float moveDuration = 0.5f;
    private Ease ease = Ease.InOutCubic;

    private float step;

    public int PanelsCount => panels.Length;

    public CarouselView(
        RectTransform content,
        RectTransform[] panels,
        float spacing,
        float moveDuration,
        Ease ease)
    {
        this.content = content;
        this.panels = panels;
        this.spacing = spacing;
        this.moveDuration = moveDuration;
        this.ease = ease;

        LayoutPanels();
        step = panels[0].rect.width + spacing;
    }

    private void LayoutPanels()
    {
        float x = 0f;
        foreach (RectTransform panel in panels)
        {
            panel.anchoredPosition = new Vector2(x, 0f);
            x += panel.rect.width + spacing;
        }
    }

    public void MoveTo(int index)
    {
        Vector2 targetPos = new Vector2(-step * index, content.anchoredPosition.y);

        content.DOAnchorPos(targetPos, moveDuration)
               .SetEase(ease);
    }
}