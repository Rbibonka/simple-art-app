public class GalleryButtonModel
{
    public int Index => index;

    public bool IsLoading => isLoading;

    public bool IsPremium => isPremium;

    private int index;
    private bool isPremium;
    private bool isLoading;

    public GalleryButtonModel(int index, bool isPremium)
    {
        this.index = index;
        this.isPremium = isPremium;
    }

    public void SetLoading(bool isLoading)
    {
        this.isLoading = isLoading;
    }
}