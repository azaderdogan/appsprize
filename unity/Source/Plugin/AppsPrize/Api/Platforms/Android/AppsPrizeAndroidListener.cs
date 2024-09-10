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

    // Called when AppsPrize initialization is completed
    void onInitialize()
    {
        listener.OnInitialize();
    }

    // Called when AppsPrize initialization fails
    void onInitializeFailed(string errorMessage)
    {
        listener.OnInitializeFailed(errorMessage);
    }

    // Called when rewards are updated
    void onRewardUpdate(AndroidJavaObject rewards)
    {
        List<Reward> rewardList = ConvertRewards(rewards);
        listener.OnRewardUpdate(rewardList);
    }

    // Helper function to convert AndroidJavaObject to a .NET List<Reward>
    private List<Reward> ConvertRewards(AndroidJavaObject rewards)
    {
        var rewardList = new List<Reward>();
        
        int size = rewards.Call<int>("size");
        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject rewardObj = rewards.Call<AndroidJavaObject>("get", i);
            Reward reward = new Reward
            {
                Level = rewardObj.Get<int>("level"),
                Points = rewardObj.Get<int>("points"),
                Currency = rewardObj.Get<string>("currency")
            };
            rewardList.Add(reward);
        }

        return rewardList;
    }
}
}