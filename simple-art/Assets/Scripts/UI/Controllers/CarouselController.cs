using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CarouselController : MonoBehaviour
{
    [Header("Carousel View")]
    [Header("Layout")]
    [SerializeField]
    private RectTransform content;

    [SerializeField] private RectTransform[] panels;

    [SerializeField]
    private float spacing = 0f;

    [Header("Animation")]
    [SerializeField]
    private float moveDuration = 0.5f;

    [SerializeField]
    private Ease ease = Ease.InOutCubic;

    [Header("Carousel Dots View")]
    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private Sprite inactiveSprite;

    [SerializeField]
    private Image[] dots;

    [Header("Carousel")]
    [SerializeField]
    private float switchDelay = 5f;

    private CarouselModel model;
    private CarouselView carouselView;
    private CarouselDotsView carouselDotsView;

    public void Initialize()
    {
        carouselView = new(content, panels, spacing, moveDuration, ease);
        carouselDotsView = new(activeSprite, inactiveSprite, dots);
        model = new(carouselView.PanelsCount);

        model.OnSlideChanged += HandleSlideChanged;

        carouselDotsView.ValidateCount(model.SlidesCount);
        carouselDotsView.SetActive(model.CurrentIndex);

        InvokeRepeating(nameof(NextSlide), switchDelay, switchDelay);
    }

    public void Deinitialize()
    {
        model.OnSlideChanged -= HandleSlideChanged;
    }

    private void NextSlide()
    {
        model.Next();
    }

    private void HandleSlideChanged(int index)
    {
        carouselView.MoveTo(index);
        carouselDotsView.SetActive(index);
    }
}