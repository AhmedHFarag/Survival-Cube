using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GooglePlayGames;

public enum UIsPanels
{
    MainMenu = 0,
    Shop = 1,
    Credits = 2
}
public class MainMenu : MonoBehaviour {
    public static MainMenu instance;
    public List<RectTransform> UIs;
    public UIsPanels CurrentUI = UIsPanels.MainMenu;
    public int CurrentPanel;
    public Text Score;
    public Text Coins;
    Button BackButton;
    bool InputEnabled = true;

    void Awake()
    {
        UIs[(int)CurrentUI].gameObject.SetActive(true);
        _FadeIn(UIs[(int)CurrentUI].gameObject);
        BackButton = UIs[(int)CurrentUI].GetChild(0).GetComponent<Button>();
    }
    void Start()
    {
        
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentPanel != 0)
        {
            _GoBack();
        }
        Score.text = DataHandler.Instance.GetBestScoreStr();
        Coins.text = DataHandler.Instance.GetPlayerCoinsstr();
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
        Color Tmep = new Color(1, 1, 1, 0.8f);
        do
        {
            Tmep.a -= FadingRate;
            for (int i = 0; i < ImageComponents.Length; i++)
            {
                ImageComponents[i].color = Tmep;
            }
            for (int i = 0; i < TextComponents.Length; i++)
            {
                TextComponents[i].color = Tmep;
            }
            yield return new WaitForSeconds(FadingTimeRate);
        } while (Tmep.a > 0);
        Parent.SetActive(false);
    }

    #endregion

    #region Buttons Navigation Behaviour

    public float InBetweenTime = 0.2f;
    float AnimationTime = 0.5f;

    public void _GotToPanel(int _Destination)
    {
        StartCoroutine(GoToPanel((UIsPanels)_Destination));
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
    public void _AnimateOut()
    {
        StartCoroutine(AnimateOut());
    }

    IEnumerator GoToPanel(UIsPanels _Destination)
    {
        CurrentPanel = (int)_Destination;
        InputEnabled = false;
        _FadeOut(UIs[(int)CurrentUI].gameObject);
        AnimationTime = (1 / FadingRate) * FadingTimeRate;
        yield return new WaitForSeconds(AnimationTime + InBetweenTime);
        UIs[(int)CurrentUI].gameObject.SetActive(false);
        CurrentUI = _Destination;
        UIs[(int)CurrentUI].gameObject.SetActive(true);
        _FadeIn(UIs[(int)CurrentUI].gameObject);
        InputEnabled = true;
    }

    public void _GoBack()
    {
        CurrentPanel--;
        _GotToPanel(CurrentPanel);
    }
    IEnumerator AnimateOut()
    {
        _FadeOut(UIs[(int)CurrentUI].gameObject);
        yield return new WaitForSeconds(AnimationTime + InBetweenTime);
        UIs[(int)CurrentUI].gameObject.SetActive(false);
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
    }
    #endregion

}
