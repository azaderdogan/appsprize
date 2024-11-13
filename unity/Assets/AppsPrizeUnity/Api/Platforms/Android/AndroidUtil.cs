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

    public static AndroidJavaObject ToAndroidInt(int? value)
    {
        return new AndroidJavaObject("java.lang.Integer", value);
    }

    public static long? FromAndroidLong(AndroidJavaObject obj)
    {
        long? timestamp = obj == null ? null : obj.Call<long>("longValue");
        return timestamp;
    }

    public static List<AppRewards> ConvertAppRewards(AndroidJavaObject appRewards)
    {
        var appRewardsList = new List<AppRewards>();
        int size = appRewards.Call<int>("size");
        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject appRewardObj = appRewards.Call<AndroidJavaObject>("get", i);
            
            var rewardObjList = appRewardObj.Get<AndroidJavaObject>("rewards");

            var rewards = ConvertRewards(rewardObjList);
            AppRewards appReward = new()
            {
                Rewards = rewards,
            };
            appRewardsList.Add(appReward);
        }
        return appRewardsList;
    }


    public static List<AppsPrizeNotification> ConvertNotifications(AndroidJavaObject notifications)
    {
        var notificationList = new List<AppsPrizeNotification>();
        int size = notifications.Call<int>("size");
        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject notificationItemObject = notifications.Call<AndroidJavaObject>("get", i);

            AppsPrizeNotification notification = new()
            {
                Id = notificationItemObject.Get<int>("id") ,
                CampaignId = notificationItemObject.Get<int>("campaignId") ,
                AppName = notificationItemObject.Get<string>("appName") ,
                Description = notificationItemObject.Get<string>("description") ,
                HasRead = notificationItemObject.Get<bool>("hasRead") ,
                IconUrl = notificationItemObject.Get<string>("iconUrl") ,
                Timestamp = FromAndroidLong(notificationItemObject.Get<AndroidJavaObject>("timestamp"))
            };
            notificationList.Add(notification);
        }
        return notificationList;
    }


    public static List<Reward> ConvertRewards(AndroidJavaObject rewards)
    {
        var rewardList = new List<Reward>();
                
        int size = rewards.Call<int>("size");
        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject rewardObj = rewards.Call<AndroidJavaObject>("get", i);
            Reward reward = new()
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
