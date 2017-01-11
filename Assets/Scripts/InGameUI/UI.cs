using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public delegate void GamePaused();

public delegate void GameResumed();
public class UI : MonoBehaviour
{
    public static UI Instance;

    public GameObject GameEnded;
    public Text txtScore;
    public Text txtEndScore;
    public Text txtHightScore;
    public Text Coins;
    public Text CoinsEnd;
    public Text TotalCoins;
    public Text Energy;
    public Slider EnergyBar;
    public RawImage Background;
    public GameObject WaveText;
    public Toggle _Controls0;
    public GameObject Controls0;
    public Toggle _ControlsJoyStick;
    public GameObject ControlsJoyStick;
    public Toggle _ControlsTouch;
    public int WaveNumber=1;
    Animator _anim;
    private GameObject pauseMenu;
    [SerializeField]
    private Animator pauseMenuAnim;
    public static event GamePaused Pause;
    public static event GameResumed Resume;
    bool paused = false;
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
        GameManager.PlayerDied += ShowGameEnded;
        GameManager.NewWave += StartCountDown;
        _anim = this.GetComponent<Animator>();
    }
    void Start()
    {
        paused = false;
        GameManager.Instance.ResetAll();
        TotalCoins.text = DataHandler.Instance.GetPlayerCoinsstr();
        UpdateControls();
    }

    void FixedUpdate()
    {
        txtScore.text = DataHandler.Instance.GetInGameScore().ToString();
        Coins.text = DataHandler.Instance.GetInGameCoins().ToString();
        Energy.text = DataHandler.Instance.GetInGameEnergy().ToString() + "/" + DataHandler.Instance.GetMaxEnergy().ToString();
        EnergyBar.value = (float)DataHandler.Instance.GetInGameEnergy() / (float)DataHandler.Instance.GetMaxEnergy();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1;
            GameManager.Instance.ReturnToMainMenu();
        }
    }
    private static void OnGamePaused()
    {
        var handler = Pause;
        if (handler != null) handler();
    }
    private static void OnGameResumed()
    {
        var handler = Resume;
        if (handler != null) handler();
    }
    public void ReStartGame()
    {

        //  PlayerPrefs.SetInt("AcivementScore", 0);
        paused = false;
        Time.timeScale = 1;
        GameManager.Instance.ReloadSameScene();
        DataHandler.Instance.ResetPlayerInGameData();
    }
    public void ShowGameEnded()
    {
        Time.timeScale = 0;
        paused = true;
        StartCoroutine("EndGame");
        
    }
    public void StartCountDown()
    {

        WaveText.SetActive(true);
        WaveText.GetComponent<Text>().text = "Wave " + (Enemies_Manager.Instance.GetCurrentWave()+1);
        WaveNumber += 1;
       
        _anim.SetTrigger("CountDown");

    }
    public void EndCountDown()
    {
        WaveText.SetActive(false);
    }
    void OnDisable()
    {
        GameManager.PlayerDied -= ShowGameEnded;
        GameManager.NewWave -= StartCountDown;

    }
    IEnumerator ScoreRoll()
    {
        int score = 0;
        //     txtHightScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        txtHightScore.text = DataHandler.Instance.GetBestScoreStr();

        while (score < DataHandler.Instance.GetInGameScore())
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.0001f));

            score += 10;

            txtEndScore.text = (score).ToString();

        }
        //int intBestScore = PlayerPrefs.GetInt("BestScore", 0);
        int intBestScore = DataHandler.Instance.GetBestScore();

        //Save Best Score
        if (intBestScore < score)
        {
            txtHightScore.text = score.ToString();
            DataHandler.Instance.SetBestScore(score);
            //Add To Leader Board //This Is For Test Delete from here
            GameManager.Instance.ReportScoreToLeaderBoard(score);
        }
        yield return null;
    }
    IEnumerator CoinRoll()
    {
        int coins = 0;
        DataHandler.Instance.AddCoins(DataHandler.Instance.GetInGameCoins());
        while (coins < DataHandler.Instance.GetInGameCoins())
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.0001f));
            coins += 10;
            CoinsEnd.text = coins.ToString();
        }

        TotalCoins.text = DataHandler.Instance.GetPlayerCoinsstr();
    }
    IEnumerator EndGame()
    {
        
        Background.gameObject.SetActive(true);
        while(Background.color.a<1)
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.01f));
            Background.color = new Vector4(0, 0, 0, Background.color.a + 0.01f);
        }
        GameEnded.SetActive(true);
        AdManager.Instance.ShowUnityVideo();
        StartCoroutine("ScoreRoll");
        StartCoroutine("CoinRoll");
        
        yield return null;
    }
    public void UpdateControls()
    {
        if (_Controls0.isOn)
        {
            Controls0.SetActive(true);
            ControlsJoyStick.SetActive(false);
            InputManager.Instance.ControlSchemeArrows = true;
            InputManager.Instance.ControlSchemeSingleButton = false;
            InputManager.Instance.ControlSchemeTouch = false;
            InputManager.Instance.ControlSchemeJoyStick = false;
        }
        else if (_ControlsJoyStick.isOn)
        {
            Controls0.SetActive(false);
            ControlsJoyStick.SetActive(true);
            InputManager.Instance.ControlSchemeArrows = false;
            InputManager.Instance.ControlSchemeSingleButton = false;
            InputManager.Instance.ControlSchemeTouch = false;
            InputManager.Instance.ControlSchemeJoyStick = true;
        }
        else if (_ControlsTouch)
        {
            Controls0.SetActive(false);
            ControlsJoyStick.SetActive(false);
            InputManager.Instance.ControlSchemeArrows = false;
            InputManager.Instance.ControlSchemeSingleButton = false;
            InputManager.Instance.ControlSchemeTouch = true;
            InputManager.Instance.ControlSchemeJoyStick = false;
        }
    }
    public void ReloadSameScene()
    {
        Time.timeScale = 1;
        paused = false;
        GameManager.Instance.ReloadSameScene();

    }
    public void ReturnToMainmenu()
    {

        Time.timeScale = 1;
        GameManager.Instance.ReturnToMainMenu();
        //MainMenu.Instance.InputEnabled = true;

    }
    public void ShowAd()
    {
        AdManager.Instance.ShowVungleRewardedAd();
    }
       public void ShowAdToContinue()
    {
        AdManager.Instance.ShowVungleRewardedAd();
        Time.timeScale = 1;
        GameManager.Instance.StartGame(Enemies_Manager.Instance.GetCurrentWave(), Enemies_Manager.Instance.GetCurrentLevel());
    }
    public void PauseButton()
    {
        if (paused)
        {
            OnGameResumed();
            ResumeGame();
        }
        else
        {
            OnGamePaused();

            StartCoroutine(PauseGame());
            
        }
    }
    IEnumerator PauseGame()
    {
        Time.timeScale = 0;
        Background.gameObject.SetActive(true);
        while (Background.color.a < 1)
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.01f));
            Background.color = new Vector4(0, 0, 0, Background.color.a + 0.1f);
        }
        pauseMenuAnim.SetTrigger("isMenuDown");
        paused = true;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        Background.gameObject.SetActive(false);
        pauseMenuAnim.SetTrigger("isMenuUp");
        paused = false;
    }
}

public static class CoroutineUtilities
{
    public static IEnumerator WaitForRealTime(float delay)
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }
    }
}