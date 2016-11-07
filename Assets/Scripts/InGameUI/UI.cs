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
    public GameObject CountDown;
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
    }

    void FixedUpdate()
    {
        txtScore.text = GameManager.Instance.score.ToString();
        Coins.text = GameManager.Instance.InGameCoins.ToString();
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
        CountDown.SetActive(true);
        _anim.SetTrigger("CountDown");
    }
    public void EndCountDown()
    {
        CountDown.SetActive(false);
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
        while (score!=GameManager.Instance.score)
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
        GameManager.Instance.Coins += GameManager.Instance.InGameCoins;
        PlayerPrefs.SetInt("Coins", GameManager.Instance.Coins);
        while (coins!=GameManager.Instance.InGameCoins)
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.0001f));
            coins += 1;
            CoinsEnd.text = coins.ToString();
        }
        
        TotalCoins.text = GameManager.Instance.Coins.ToString();

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