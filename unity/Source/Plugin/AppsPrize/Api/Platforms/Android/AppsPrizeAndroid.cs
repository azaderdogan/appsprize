using System;
using System.Collections.Generic;
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

        public static void Initialize(AppsPrizeConfig config, IAppsPrizeListener listener)
        {
            Debug.Log("[Unity-AppsPrize]: Initialize");
            AndroidJavaObject androidConfig = AppsPrizeConfigAndroid.Create(config);

            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic("initialize", unityActivity, androidConfig, new AppsPrizeListener(listener));
            }));
           
        }

        public static void DoReward(Action<List<Reward>> onSessionRewardCallback)
        {
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                appsPrizeClass.CallStatic("doReward", unityActivity, new AppsPrizeRewardListener(onSessionRewardCallback));
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

