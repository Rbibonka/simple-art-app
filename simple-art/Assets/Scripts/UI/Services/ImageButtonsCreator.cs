using System.Collections.Generic;
using UnityEngine;

public class ImageButtonsCreator
{
    private BaseObjectPool<GalleryButtonController> galleryButtonPool;
    private GalleryButtonController imageButtonPrefab;

    private RectTransform content;
    private List<GalleryButtonController> imageButtons;

    private List<int> oddIndexes;
    private List<int> evenIndexes;
    private List<int> fullIndexes;

    public ImageButtonsCreator(GalleryButtonController imageButtonPrefab, RectTransform content)
    {
        this.imageButtonPrefab = imageButtonPrefab;
        this.content = content;

        imageButtons = new();

        oddIndexes = new();
        evenIndexes = new();
        fullIndexes = new();

        CreateIndexes();
    }

    public List<GalleryButtonController> CreateOddImageButtons()
    {
        return CreateImageButtons(oddIndexes);
    }

    public List<GalleryButtonController> CreateEvenImageButtons()
    {
        return CreateImageButtons(evenIndexes);
    }

    public List<GalleryButtonController> CreateFullImageButtons()
    {
        return CreateImageButtons(fullIndexes);
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

            fullIndexes.Add(i);
        }
    }

    private List<GalleryButtonController> CreateImageButtons(List<int> indexes)
    {
        galleryButtonPool = new(imageButtonPrefab, content);

        for (int i = 1; i < indexes.Count; i++)
        {
            var item = galleryButtonPool.GetFromPool();

            bool isPremium = false;

            if (i % 4 == 0)
            {
                isPremium = true;
            }

            item.Initialize(indexes[i], isPremium);
            imageButtons.Add(item);
        }

        return imageButtons;
    }
}