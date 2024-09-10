using AppsPrizeUnity.Platforms.Android;

namespace AppsPrizeUnity
{
    public static class AppsPrize
    {

        public static void Initialize()
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.Initialize();
            #endif
        }

        // // Reward session with listener
        // public static void DoReward(AppsPrizeRewardListener rewardListener)
        // {
        //     appsPrizeClass.CallStatic("doReward", unityActivity, rewardListener);
        // }

        public static void LaunchActivity()
        {
            #if UNITY_ANDROID
                AppsPrizeAndroid.LaunchActivity();
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
}

