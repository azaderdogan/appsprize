using UnityEngine;

public static class AndroidUtil
{
    public static AndroidJavaObject ToAndroidColor(Color unityColor)
    {
        int alpha = Mathf.RoundToInt(unityColor.a * 255f);
        int red = Mathf.RoundToInt(unityColor.r * 255f);
        int green = Mathf.RoundToInt(unityColor.g * 255f);
        int blue = Mathf.RoundToInt(unityColor.b * 255f);

        int androidColor = (alpha << 24) | (red << 16) | (green << 8) | blue;
        return new AndroidJavaObject("java.lang.Integer", androidColor);
    }
}
