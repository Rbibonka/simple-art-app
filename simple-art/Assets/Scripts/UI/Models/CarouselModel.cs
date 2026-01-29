using System;

public class CarouselModel
{
    public int CurrentIndex { get; private set; }

    public int SlidesCount { get; }

    public event Action<int> OnSlideChanged;

    public CarouselModel(int slidesCount)
    {
        SlidesCount = slidesCount;
        CurrentIndex = 0;
    }

    public void Next()
    {
        CurrentIndex++;
        if (CurrentIndex >= SlidesCount)
        {
            CurrentIndex = 0;
        }

        OnSlideChanged?.Invoke(CurrentIndex);
    }

    public void SetIndex(int index)
    {
        if (index < 0 || index >= SlidesCount)
        {
            return;
        }

        CurrentIndex = index;
        OnSlideChanged?.Invoke(CurrentIndex);
    }
}