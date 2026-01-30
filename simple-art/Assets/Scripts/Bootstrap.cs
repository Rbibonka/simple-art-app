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

    [Header("Debug")]
    [SerializeField]
    private bool debugMode;

    [SerializeField]
    private bool isTablet;

    private async void Awake()
    {
        if (debugMode)
        {
            await galleryButtonsController.InitializeAsync(tabBarController, isTablet);
        }
        else
        {
            await galleryButtonsController.InitializeAsync(tabBarController, DeviceUtils.IsTablet());
        }

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