using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private GalleryButtonsController galleryButtonsController;

    [SerializeField]
    private CarouselController carouselController;

    private async void Awake()
    {
        carouselController.Initialize();
        await galleryButtonsController.InitializeAsync();
    }

    private void OnDestroy()
    {
        galleryButtonsController.Deinitialize();
        carouselController.Deinitialize();
    }
}