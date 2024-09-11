using System.Collections.Generic;
using AppsPrizeUnity;
using UnityEngine;

internal static class AndroidUtil
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

    public static List<Reward> ConvertRewards(AndroidJavaObject rewards)
    {
        var rewardList = new List<Reward>();
        
        int size = rewards.Call<int>("size");
        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject rewardObj = rewards.Call<AndroidJavaObject>("get", i);
            Reward reward = new Reward
            {
                Level = rewardObj.Get<int>("level"),
                Points = rewardObj.Get<int>("points"),
                Currency = rewardObj.Get<string>("currency")
            };
            rewardList.Add(reward);
        }

        return rewardList;
    }
}
