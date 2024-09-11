using System;
using System.Collections.Generic;
using UnityEngine;


namespace AppsPrizeUnity.Platforms.Android
{
    public class AppsPrizeListener : AndroidJavaProxy
    {
        private readonly IAppsPrizeListener listener;

        public AppsPrizeListener(IAppsPrizeListener listener)
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
            Debug.Log("Internal: onRewardUpdate: " + rewards);
            List<AppRewards> rewardList = AndroidUtil.ConvertAppRewards(rewards);
            listener.OnRewardUpdate(rewardList);
        }
    }

    public class AppsPrizeRewardListener : AndroidJavaProxy
    {
        private readonly Action<List<AppRewards>> onSessionRewardCallback;

        public AppsPrizeRewardListener(Action<List<AppRewards>> callback) : base("com.appsamurai.appsprize.AppsPrizeRewardListener")
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