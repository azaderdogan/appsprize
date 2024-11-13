using System;
using System.Collections.Generic;
using UnityEngine;


namespace AppsPrizeUnity.Platforms.Android
{
    public class AppsPrizeAndroidListener : AndroidJavaProxy
    {
        private readonly IAppsPrizeListener listener;

        public AppsPrizeAndroidListener(IAppsPrizeListener listener)
            : base("com.appsamurai.appsprize.AppsPrizeListener")
        {
            this.listener = listener;
        }

        void onInitialize()
        {
            listener.OnInitialize();
        }

        void onInitializeFailed(string errorMessage)
        {
            listener.OnInitializeFailed(errorMessage);
        }

        void onRewardUpdate(AndroidJavaObject rewards)
        {
            List<AppRewards> rewardList = AndroidUtil.ConvertAppRewards(rewards);
            listener.OnRewardUpdate(rewardList);
        }

        void onNotification(AndroidJavaObject notifications)
        {
            List<AppsPrizeNotification> notificationList = AndroidUtil.ConvertNotifications(notifications);
            listener.OnNotification(notificationList);
        }
    }

    public class AppsPrizeRewardAndroidListener : AndroidJavaProxy
    {
        private readonly AppsPrizeRewardListener onSessionRewardCallback;

        public AppsPrizeRewardAndroidListener(AppsPrizeRewardListener callback) : base("com.appsamurai.appsprize.AppsPrizeRewardListener")
        {
            onSessionRewardCallback = callback;
        }

        void onSessionReward(AndroidJavaObject rewards)
        {
            List<AppRewards> rewardList = AndroidUtil.ConvertAppRewards(rewards);
            onSessionRewardCallback?.Invoke(rewardList);
        }
    }
}