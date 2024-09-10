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

        // // Reward session with listener
        // public static void DoReward(AppsPrizeRewardListener rewardListener)
        // {
        //     appsPrizeClass.CallStatic("doReward", unityActivity, rewardListener);
        // }

        public static void Launch()
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.Launch();
            #endif
        }

        // // Check if has permissions
        // public static bool HasPermissions()
        // {
        //     return appsPrizeClass.CallStatic<bool>("hasPermissions", unityActivity);
        // }

        // // Request permissions
        // public static bool RequestPermission()
        // {
        //     return appsPrizeClass.CallStatic<bool>("requestPermission", unityActivity);
        // }
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

