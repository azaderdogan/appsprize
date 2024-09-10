using UnityEngine;


namespace AppsPrizeUnity.Platforms.Android
{
    public static class AppsPrizeAndroid
    {
        private static AndroidJavaClass appsPrizeClass;
        private static AndroidJavaObject unityActivity;

        static AppsPrizeAndroid()
        {
            Debug.Log("[Unity-AppsPrize]: static-load");
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            appsPrizeClass = new AndroidJavaClass("com.appsamurai.appsprize.AppsPrize");
        }

        // Initialize AppsPrize with AppsPrizeConfig and listener
        public static void Initialize()
        {
            Debug.Log("[Unity-AppsPrize]: Initialize");
            AndroidJavaObject config = AppsPrizeConfigAndroid.CreateConfig("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTE5fQ.veS-GapCh2LnUkZWddiPWfb8DLEWvyE2VqA-mMKRESM", "asfsdf", "asdfadsf");

            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic("initialize", unityActivity, config, null);
            }));
           
        }

        // // Reward session with listener
        // public static void DoReward(AppsPrizeRewardListener rewardListener)
        // {
        //     appsPrizeClass.CallStatic("doReward", unityActivity, rewardListener);
        // }

        // Launch AppsPrize Activity
        public static void LaunchActivity()
        {
            Debug.Log("[Unity-AppsPrize]: LaunchActivity");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic<bool>("launchActivity", unityActivity);
            }));
        }

        // // Get Activity Intent
        // public static AndroidJavaObject GetActivityIntent()
        // {
        //     return appsPrizeClass.CallStatic<AndroidJavaObject>("getActivityIntent", unityActivity);
        // }

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

