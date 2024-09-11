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

        public static void DoReward(Action<List<Reward>> onSessionRewardCallback)
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
        void OnRewardUpdate(List<Reward> rewards);
    }

    public class Reward
    {
        public int Level { get; set; }
        public int Points { get; set; }
        public string Currency { get; set; }
    }
}

