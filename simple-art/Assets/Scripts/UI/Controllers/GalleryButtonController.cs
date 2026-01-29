using UnityEngine;
using UnityEngine.UI;

public class GalleryButtonController : MonoBehaviour
{
    public int ImageIndex => imageIndex;

    public bool IsLoading => isLoading;

    [SerializeField]
    private Image image;

    private int imageIndex;

    private bool isLoading;

    public void Initialize(int imageIndex)
    {
        this.imageIndex = imageIndex;
    }

    public void StartLoading()
    {
        isLoading = true;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}