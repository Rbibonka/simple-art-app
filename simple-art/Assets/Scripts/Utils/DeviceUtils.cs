using UnityEngine;

public static class DeviceUtils
{
    public static bool IsTablet()
    {
        float dpi = Screen.dpi;

        if (dpi == 0)
            return false;

        float widthInches = Screen.width / dpi;
        float heightInches = Screen.height / dpi;

        float diagonal = Mathf.Sqrt(
            widthInches * widthInches +
            heightInches * heightInches
        );

        return diagonal >= 7f;
    }
}