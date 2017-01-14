using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.Events;

public class MainMenuSliders : MonoBehaviour {
    public static MainMenuSliders Instance;
    public Text Score;
    public List<Text> Coins;
    Button BackButton;
    bool InputEnabled = true;

    public GameObject Msgbox;
    public GameObject EwaponInfoOverlap;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Msgbox.SetActive(true);
        EwaponInfoOverlap.SetActive(true);
        DataHandler.Instance.MainMenuWasLoaded();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _GoBack();
        }
        if(Score)
        Score.text = DataHandler.Instance.GetBestScoreStr();
        foreach (var item in Coins)
        {
            if(item)
            item.text = DataHandler.Instance.GetPlayerCoinsstr();
        }
        
    }
    public void StartGame()
    {
        GameManager.Instance.GoToLevelSelect();
    }

    #region Buttons Navigation Behaviour

    public void _GotToPanel(int _Destination)
    {
    }
    public void _GoBack()
    {

    }


    #endregion

    #region Buttons Functionality

    public void _ExitGame()
    {
        Application.Quit();
    }
    public void ToAchievementUI()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            //Log Connection Error
        }
    }
    public void ToLeaderBoardUI()
    {
#if UNITY_EDITOR

#else

        if (Social.localUser.authenticated)
        {
            //Show All LeaderBoards
            //Social.ShowLeaderboardUI();
            //Show only top player
            PlayGamesPlatform.Instance.ShowLeaderboardUI(SurvivalCubeResources.leaderboard_topplayers);
        }
        else
        {
            //Log Connection Error
        }
#endif
    }
    public void ResetAllData()
    {
        DataHandler.Instance.ResetAllPlayerSavedData();
    }
    public void UnlockNewInGameTempWeaponSlot()
    {

        if (DataHandler.Instance.GetPlayerCoins() >= 500)
        {


            MSGScript.Instance.Choice("Would you To Add New Slot??", new UnityAction(() =>
            {
                DataHandler.Instance.AddCoins(-500);
                //Save unlocked Main weapon 
                DataHandler.Instance.UnlockNewTempWeaponSlot();
            })
           , new UnityAction(() => { }), new UnityAction(() => { }));


        }
        else
        {
            MSGScript.Instance.OK("You Do not have enough Coins!!", () =>
            {

            });
        }
        
    }
#endregion
    public void ShowADAndGetCoins()
    {
        DataHandler.Instance.AddCoins(10000);
        //AdManager.Instance.ShowUnityRewardedAd();
    }

}
