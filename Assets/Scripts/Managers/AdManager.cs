using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using System.Collections;
using System.Collections.Generic;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    string gameID = "1225124";
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
#if UNITY_ADS
        Advertisement.Initialize(gameID);
#endif
        Vungle.init("585922924db489ea1e000877", "Test_iOS", "Test_Windows");
        Vungle.onAdFinishedEvent += (AdFinishedEventArgs) =>
        {
            //HandleShowResult(ShowResult.Finished);
            DataHandler.Instance.AddInGameCoins(50);
        };
    }
    public void ShowUnityAd()
    {
#if UNITY_ADS

        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
#endif
    }
    public void ShowUnityVideo()
    {
#if UNITY_ADS

        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
#endif
    }
    public void ShowUnityRewardedAd()
    {
#if UNITY_ADS

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
#endif
    }
    public void ShowVungleAd()
    {
        Vungle.playAd();
    }
    public void ShowVungleRewardedAd()
    {
        Dictionary<string, object> options = new Dictionary<string, object>();
        options["incentivized"] = true;
        Vungle.playAdWithOptions(options);
        
    }
#if UNITY_ADS
    
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                DataHandler.Instance.AddCoins(50);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }

    }
#endif
}
public class AdFinishedEventArgs : EventArgs
{
    // true if a user tapped Download button to go store
    public bool WasCallToActionClicked;

    // true if at least 80% of the video was watched
    public bool IsCompletedView;

    // duration a Vungle ad watched
    public double TimeWatched;

    // total duration of a Vungle ad
    public double TotalDuration;
}
