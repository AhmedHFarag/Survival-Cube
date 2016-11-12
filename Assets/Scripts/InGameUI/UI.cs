using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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
    public RawImage Background;
    public GameObject WaveText;
    public Toggle _Controls0;
    public GameObject Controls0;
    public Toggle _ControlsJoyStick;
    public GameObject ControlsJoyStick;
    public int WaveNumber=1;
    Animator _anim;
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
        GameManager.Instance.ResetAll();
        TotalCoins.text = PlayerPrefs.GetInt("Coins").ToString();
        UpdateControls();
    }

    void FixedUpdate()
    {
        //  txtScore.text = GameManager.Instance.score.ToString();
        txtScore.text = DataHandler.Instance.AcivementScore.ToString();
        //  Coins.text = GameManager.Instance.InGameCoins.ToString();
        Coins.text = DataHandler.Instance.inGameCoins.ToString();
    }
    public void ReStartGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.ReloadSameScene();

    }
    public void ShowGameEnded()
    {
        Time.timeScale = 0;
        StartCoroutine("EndGame");
        
    }
    public void StartCountDown()
    {

        WaveText.SetActive(true);
        WaveText.GetComponent<Text>().text = "Wave " + WaveNumber;
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
        txtHightScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
     //   while (score!=GameManager.Instance.score)
     while(score!=DataHandler.Instance.AcivementScore)
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.0001f));

            score += 1;

            txtEndScore.text = (score).ToString();
            
        }
        int intBestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (intBestScore < score)
        {
            //bestScore.text = lastScore.text;
            PlayerPrefs.SetInt("BestScore", score);
        }
        yield return null;
    }
    IEnumerator CoinRoll()
    {
        int coins = 0;
        //   GameManager.Instance.Coins += GameManager.Instance.InGameCoins;
        DataHandler.Instance.playerCoins += DataHandler.Instance.inGameCoins;
        //  PlayerPrefs.SetInt("Coins", GameManager.Instance.Coins);
        PlayerPrefs.SetInt("PlayerCoins", DataHandler.Instance.playerCoins);
    //    while (coins!=GameManager.Instance.InGameCoins)
    while(coins!=DataHandler.Instance.inGameCoins)
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.0001f));
            coins += 1;
            CoinsEnd.text = coins.ToString();
        }

        //  TotalCoins.text = GameManager.Instance.Coins.ToString();
        TotalCoins.text = DataHandler.Instance.playerCoins.ToString();

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
            InputManager.Instance.ControlScheme0 = true;
            InputManager.Instance.ControlScheme1 = false;
            InputManager.Instance.ControlScheme2 = false;
            InputManager.Instance.ControlScheme3 = false;
        }
        else if (_ControlsJoyStick.isOn)
        {
            Controls0.SetActive(false);
            ControlsJoyStick.SetActive(true);
            InputManager.Instance.ControlScheme0 = false;
            InputManager.Instance.ControlScheme1 = false;
            InputManager.Instance.ControlScheme2 = false;
            InputManager.Instance.ControlScheme3 = true;
        }
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