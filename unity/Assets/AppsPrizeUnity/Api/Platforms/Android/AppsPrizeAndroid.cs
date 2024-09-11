using System;
using System.Collections.Generic;
using UnityEngine;


namespace AppsPrizeUnity.Platforms.Android
{
    public static class AppsPrizeAndroid
    {
        private static readonly AndroidJavaClass appsPrizeClass;
        private static readonly AndroidJavaObject unityActivity;

        static AppsPrizeAndroid()
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            appsPrizeClass = new AndroidJavaClass("com.appsamurai.appsprize.AppsPrize");
        }

        public static void Initialize(AppsPrizeConfig config, IAppsPrizeListener listener)
        {
            AndroidJavaObject androidConfig = AppsPrizeConfigAndroid.Create(config);

            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic("initialize", unityActivity, androidConfig, new AppsPrizeAndroidListener(listener));
            }));
           
        }

        public static void DoReward(AppsPrizeRewardListener onSessionRewardCallback)
        {
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic("doReward", unityActivity, new AppsPrizeRewardAndroidListener(onSessionRewardCallback));
            }));
        }

        public static void Launch()
        {
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic<bool>("launchActivity", unityActivity);
            }));
        }

        public static bool HasPermissions()
        {
            return appsPrizeClass.CallStatic<bool>("hasPermissions", unityActivity);
        }

        public static bool RequestPermission()
        {
            return appsPrizeClass.CallStatic<bool>("requestPermission", unityActivity);
        }
    }
}

