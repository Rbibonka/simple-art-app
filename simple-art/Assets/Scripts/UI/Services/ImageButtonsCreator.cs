using System.Collections.Generic;
using UnityEngine;

public class ImageButtonsCreator
{
    private BaseObjectPool<GalleryButtonController> galleryButtonPool;
    private List<GalleryButtonController> imageButtons;

    private RectTransform content;

    private List<int> oddIndexes;
    private List<int> evenIndexes;
    private List<int> allIndexes;

    public ImageButtonsCreator(GalleryButtonController imageButtonPrefab, RectTransform content, RectTransform parent)
    {
        this.content = content;

        imageButtons = new();

        oddIndexes = new();
        evenIndexes = new();
        allIndexes = new();

        galleryButtonPool = new(imageButtonPrefab, parent);

        CreateIndexes();
    }

    public void DeleteCurrentImageButtons()
    {
        for (int i = 0; i < imageButtons.Count; i++)
        {
            imageButtons[i].gameObject.SetActive(false);
            imageButtons[i].ResetSprite();
            galleryButtonPool.SetToPool(imageButtons[i]);
        }

        imageButtons.Clear();
    }

    public List<GalleryButtonController> CreateOddImageButtons()
    {
        DeleteCurrentImageButtons();

        return CreateImageButtons(oddIndexes);
    }

    public List<GalleryButtonController> CreateEvenImageButtons()
    {
        DeleteCurrentImageButtons();
        return CreateImageButtons(evenIndexes);
    }

    public List<GalleryButtonController> CreateAllImageButtons()
    {
        DeleteCurrentImageButtons();
        return CreateImageButtons(allIndexes);
    }

    private void CreateIndexes()
    {
        for (int i = 1; i < 66; i++)
        {
            if (i % 2 == 0)
            {
                evenIndexes.Add(i);
            }
            else
            {
                oddIndexes.Add(i);
            }

            allIndexes.Add(i);
        }
    }

    private List<GalleryButtonController> CreateImageButtons(List<int> indexes)
    {
        for (int i = 0; i < indexes.Count; i++)
        {
            var item = galleryButtonPool.GetFromPool();
            item.gameObject.SetActive(true);
            item.transform.SetParent(content, false);

            bool isPremium = false;

            if ((i + 1) % 4 == 0)
            {
                isPremium = true;
            }

            item.Initialize(indexes[i], isPremium);
            imageButtons.Add(item);
        }

        return imageButtons;
    }
}