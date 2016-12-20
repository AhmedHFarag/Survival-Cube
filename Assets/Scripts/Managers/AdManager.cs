using UnityEngine;
using UnityEngine.Advertisements;
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
        Advertisement.Initialize(gameID, true);
        Vungle.init("585922924db489ea1e000877", "Test_iOS", "Test_Windows");
        Vungle.onAdFinishedEvent += (AdFinishedEventArgs) =>
        {
            //HandleShowResult(ShowResult.Finished);
            DataHandler.Instance.AddInGameCoins(50);
        };
    }
    public void ShowUnityAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
    public void ShowUnityVideo()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }
    public void ShowUnityRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
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
