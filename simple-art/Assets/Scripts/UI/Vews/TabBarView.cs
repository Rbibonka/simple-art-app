using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TabBarView
{
    private RectTransform selector;

    private float animationTime = 0.4f;

    public TabBarView(RectTransform selector)
    {
        this.selector = selector;
    }

    public void MoveSelectorTo(Button targetRect)
    {
        var targetPosition = new Vector3(targetRect.transform.position.x, selector.transform.position.y, selector.transform.position.z);
        selector.DOMove(targetPosition, animationTime);
        RectTransform rect = targetRect.GetComponent<RectTransform>();

        float targetWidth = rect.rect.width;

        selector.DOSizeDelta(
            new Vector2(targetWidth, selector.sizeDelta.y),
            animationTime
        ).SetEase(Ease.OutCubic);
    }
}