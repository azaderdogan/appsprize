using UnityEngine;

using AppsPrizeUnity;
using System.Collections.Generic;
using System;


public class InitializeAppsPrizeScript : MonoBehaviour, IAppsPrizeListener
{
    [SerializeField]
    private NotifyTextScript notifyText;

    public void Start()
    {
        AppsPrize.Initialize(
            new AppsPrizeConfig(
                token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MX0.6YZeCXIC7StDO4wf1m0wQusrVR8ZwxzXIKFVUDYLKP4",
                advertisingId: "AA1111AA-A111-11AA-A111-11AAA1A11111",
                userId: "1111-5",
                country: "US",
                language: "en",
                gender: "MALE",
                age: 30,
                uaChannel: "test uaChannel",
                uaNetwork: "test uaNetwork",
                adPlacement: "test adPlacement"
            ),
            this
        );

        // AppsPrize.DoReward(OnCurrentRewardUpdate);

        Debug.Log("[AppsPrize-Unity]: HasPermissions" + AppsPrize.HasPermissions());
        Debug.Log("[AppsPrize-Unity]: " + AppsPrize.RequestPermission());
        notifyText.Log("[AppsPrize-Unity]: Try initialize");
	}

    public void OnInitialize()
    {
        Debug.Log("[AppsPrize-Unity]: OnInitialize");
        notifyText.Log("[AppsPrize-Unity]: OnInitialize");
    }

    public void OnInitializeFailed(string errorMessage)
    {
        Debug.Log("[AppsPrize-Unity]: OnInitializeFailed");
        notifyText.Log("[AppsPrize-Unity]: OnInitializeFailed:" + errorMessage);
    }

    public void OnRewardUpdate(List<AppRewards> rewards)
    {
        Debug.Log("[AppsPrize-Unity]: OnRewardUpdate: " + rewards.ToString());
        notifyText.Log("[AppsPrize-Unity]: OnRewardUpdate: " + rewards.ToString());
    }

    public void OnNotification(List<AppsPrizeNotification> notifications)
    {
         Debug.Log("[AppsPrize-Unity]: OnNotification: " + notifications.ToString());
         notifyText.Log("[AppsPrize-Unity]: OnNotification: " + notifications.ToString());
    }


    // public void OnCurrentRewardUpdate(List<AppRewards> rewards)
    // {
    //     notifyText.Log("[AppsPrize-Unity]: Current rewards: " + rewards.ToString());
    // }

}

