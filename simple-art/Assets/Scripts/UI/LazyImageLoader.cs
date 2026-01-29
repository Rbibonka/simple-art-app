using UnityEngine;
using UnityEngine.UI;

public class LazyImageLoader : MonoBehaviour
{
    public int ImageIndex => imageIndex;

    public bool IsLoaded => isLoaded;

    [SerializeField]
    private Image image;

    private int imageIndex;

    private bool isLoaded;

    public void Initialize(int imageIndex)
    {
        this.imageIndex = imageIndex;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        isLoaded = true;
    }
}