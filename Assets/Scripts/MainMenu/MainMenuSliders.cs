using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GooglePlayGames;

public class MainMenuSliders : MonoBehaviour {
    public static MainMenuSliders Instance;
    public Text Score;
    public List<Text> Coins;
    Button BackButton;
    bool InputEnabled = true;

    public GameObject Msgbox;
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
        GameManager.Instance.StartGame();
    }
    #region Fading Animation Funxtionality

    public float FadingRate = 0.1f;
    public float FadingTimeRate = 0.1f;

    public void _FadeIn(GameObject Parent)
    {
        StartCoroutine(FadeIn(Parent));
    }

    IEnumerator FadeIn(GameObject Parent)
    {
        Parent.SetActive(true);
        Image[] ImageComponents = Parent.GetComponentsInChildren<Image>();
        Text[] TextComponents = Parent.GetComponentsInChildren<Text>();
        float a = 0;
        do
        {
            a += FadingRate;
            for (int i = 0; i < ImageComponents.Length; i++)
            {
                ImageComponents[i].color =new Color(ImageComponents[i].color.r, ImageComponents[i].color.g, ImageComponents[i].color.b, a);
            }
            for (int i = 0; i < TextComponents.Length; i++)
            {
                TextComponents[i].color = new Color(TextComponents[i].color.r, TextComponents[i].color.g, TextComponents[i].color.b, a);
            }
            yield return new WaitForSeconds(FadingTimeRate);
        } while (a < 1);
        Button[] ButtonComponents = Parent.GetComponentsInChildren<Button>();
        for (int i = 0; i < ButtonComponents.Length; i++)
        {
            if (ButtonComponents[i].name[0] == 'D')
            {
                ButtonComponents[i].interactable = false;
            }
            ButtonComponents[i].interactable = true;
        }
    }

    public void _FadeOut(GameObject Parent)
    {
        StartCoroutine(FadeOut(Parent));
    }

    IEnumerator FadeOut(GameObject Parent)
    {
        Button[] ButtonComponents = Parent.GetComponentsInChildren<Button>();
        for (int i = 0; i < ButtonComponents.Length; i++)
        {
            ButtonComponents[i].interactable = false;
        }
        Image[] ImageComponents = Parent.GetComponentsInChildren<Image>();
        Text[] TextComponents = Parent.GetComponentsInChildren<Text>();
        float a = 0;
        do
        {
            a -= FadingRate;
            for (int i = 0; i < ImageComponents.Length; i++)
            {
                ImageComponents[i].color = new Color(ImageComponents[i].color.r, ImageComponents[i].color.g, ImageComponents[i].color.b, a);
            }
            for (int i = 0; i < TextComponents.Length; i++)
            {
                TextComponents[i].color = new Color(TextComponents[i].color.r, TextComponents[i].color.g, TextComponents[i].color.b, a);
            }
            yield return new WaitForSeconds(FadingTimeRate);
        } while (a > 0);
        Parent.SetActive(false);
    }

    #endregion

    #region Buttons Navigation Behaviour

    public float InBetweenTime = 0.2f;
    float AnimationTime = 0.5f;

    public void _GotToPanel(int _Destination)
    {
    }

    public void StartLoading(int _Destination)
    {
        if (InputEnabled)
        {
            InputEnabled = false;
            StartCoroutine(ExecuteAfterTime(3.5f, _Destination));
        }
    }
    IEnumerator ExecuteAfterTime(float time, int _Destination)
    {
        yield return new WaitForSeconds(time);
        _GotToPanel(_Destination);
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
#endregion
    public void ShowADAndGetCoins()
    {
        AdManager.Instance.ShowUnityRewardedAd();
    }

}
