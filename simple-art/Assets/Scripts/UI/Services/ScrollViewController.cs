using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController
{
    public event Action<GalleryButtonController> imageButtonIsVisible;

    private ScrollRect scrollRect;
    private Scrollbar scrollbar;

    private IReadOnlyList<GalleryButtonController> items;

    public ScrollViewController(
        ScrollRect scrollRect,
        Scrollbar scrollbar,
        IReadOnlyList<GalleryButtonController> items)
    {
        this.scrollRect = scrollRect;
        this.scrollbar = scrollbar;
        this.items = items;

        this.scrollRect.onValueChanged.AddListener(_ => CheckVisibility());
    }

    public void InitialCheck()
    {
        CheckVisibility();
    }

    public void ResetScroll()
    {
        float target = 1;

        DOTween.To(
            () => scrollbar.value,
            x => scrollbar.value = x,
            Mathf.Clamp01(target),
            0.5f
        )
        .SetEase(Ease.OutQuad);
    }

    private void CheckVisibility()
    {
        foreach (var item in items)
        {
            if (IsVisible(item.GetComponent<RectTransform>()))
            {
                imageButtonIsVisible?.Invoke(item);
            }
        }
    }

    private bool IsVisible(RectTransform item)
    {
        Vector3[] itemCorners = new Vector3[4];
        Vector3[] viewCorners = new Vector3[4];

        item.GetWorldCorners(itemCorners);
        scrollRect.viewport.GetWorldCorners(viewCorners);

        return itemCorners[2].y > viewCorners[0].y &&
               itemCorners[0].y < viewCorners[2].y;
    }
}