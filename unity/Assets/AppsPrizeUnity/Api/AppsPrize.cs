using System;
using System.Collections.Generic;
using AppsPrizeUnity.Platforms.Android;

namespace AppsPrizeUnity
{
    public static class AppsPrize
    {

        public static void Initialize(AppsPrizeConfig config, IAppsPrizeListener listener)
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.Initialize(config, listener);
            #endif
        }

        public static void DoReward(AppsPrizeRewardListener onSessionRewardCallback)
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.DoReward(onSessionRewardCallback);
            #endif
        }

        public static void Launch()
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.Launch();
            #endif
        }

        public static void Open(int campaignId)
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.Open(campaignId);
            #endif
        }

        public static bool HasPermissions()
        {
            #if UNITY_ANDROID
                return AppsPrizeAndroid.HasPermissions();
            #else
                return false;
            #endif
            
        }

        public static bool RequestPermission()
        {
            #if UNITY_ANDROID
                return AppsPrizeAndroid.RequestPermission();
            #else
                return false;
            #endif
        }
    }

    public interface IAppsPrizeListener
    {
        void OnInitialize();
        void OnInitializeFailed(string errorMessage);
        void OnRewardUpdate(List<AppRewards> rewards);
        void OnNotification(List<AppsPrizeNotification> notifications);
    }

    public delegate void AppsPrizeRewardListener(List<AppRewards> rewards);


    public class AppRewards
    {
        public List<Reward> Rewards { get; set; }
    }

    public class Reward
    {
        public int Level { get; set; }
        public int Points { get; set; }
        public string Currency { get; set; }
    }

    public class AppsPrizeNotification
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string AppName { get; set; }
        public string Description { get; set; }
        public bool HasRead { get; set; }
        public string IconUrl { get; set; }
        public long? Timestamp { get; set; }
    }
}

