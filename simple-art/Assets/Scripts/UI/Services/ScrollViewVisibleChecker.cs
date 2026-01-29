using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewVisibleChecker
{
    public event Action<LazyImageLoader> imageButtonIsVisible;

    private ScrollRect scrollRect;

    private IReadOnlyList<LazyImageLoader> items;

    public ScrollViewVisibleChecker(
        ScrollRect scrollRect,
        IReadOnlyList<LazyImageLoader> items)
    {
        this.scrollRect = scrollRect;
        this.items = items;

        this.scrollRect.onValueChanged.AddListener(_ => CheckVisibility());
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