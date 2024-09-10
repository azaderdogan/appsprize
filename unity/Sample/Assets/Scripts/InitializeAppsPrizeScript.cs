using UnityEngine;

using AppsPrizeUnity;
using System.Collections.Generic;

public class InitializeAppsPrizeScript : MonoBehaviour, IAppsPrizeListener
{
    public void Start()
    {
        AppsPrize.Initialize(
            new AppsPrizeConfig(
                token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTE5fQ.veS-GapCh2LnUkZWddiPWfb8DLEWvyE2VqA-mMKRESM",
                advertisingId: "AA1111AA-A111-11AA-A111-11AAA1A11111",
                userId: "1111",
                country: "US",
                language: "en",
                new AppsPrizeStyleConfig(
                    primaryColor: Color.blue,
                    secondaryColor: Color.black,
                    highlightColor: Color.green,
                    offersTitleText: "Deneme Offer",
                    appsTitleText: "Deneme Apps"
                )
            ),
            this
        );
    }

    public void OnInitialize()
    {
        Debug.Log("[AppsPrize-Unity]: OnInitialize");
    }

    public void OnInitializeFailed(string errorMessage)
    {
        Debug.Log("[AppsPrize-Unity]: OnInitializeFailed");
    }

    public void OnRewardUpdate(List<Reward> rewards)
    {
        Debug.Log("[AppsPrize-Unity]: OnRewardUpdate: " + rewards.ToString());
    }

}

