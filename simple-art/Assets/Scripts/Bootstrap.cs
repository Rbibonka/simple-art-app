using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private GalleryButtonsController galleryButtonsController;

    [SerializeField]
    private CarouselController carouselController;

    [SerializeField]
    private TabBarController tabBarController;

    private async void Awake()
    {
        await galleryButtonsController.InitializeAsync(tabBarController);
        carouselController.Initialize();
        tabBarController.Initialize();
    }

    private void OnDestroy()
    {
        galleryButtonsController.Deinitialize();
        tabBarController.Deinitialize();
        carouselController.Deinitialize();
    }
}