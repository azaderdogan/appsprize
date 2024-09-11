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
            List<Reward> rewardList = AndroidUtil.ConvertRewards(rewards);
            listener.OnRewardUpdate(rewardList);
        }
    }

    public class AppsPrizeRewardListener : AndroidJavaProxy
    {
        private readonly Action<List<Reward>> onSessionRewardCallback;

        public AppsPrizeRewardListener(Action<List<Reward>> callback) : base("com.appsamurai.appsprize.AppsPrizeRewardListener")
        {
            onSessionRewardCallback = callback;
        }

        void onSessionReward(AndroidJavaObject rewards)
        {
            List<Reward> rewardList = AndroidUtil.ConvertRewards(rewards);
            onSessionRewardCallback?.Invoke(rewardList);
        }
    }
}