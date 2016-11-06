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
    public RawImage Background;

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
        GameManager.Instance.ResetAll();
        GameManager.PlayerDied += ShowGameEnded;
    }

    void FixedUpdate()
    {
        txtScore.text = GameManager.Instance.score.ToString();
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
    void OnDisable()
    {
        GameManager.PlayerDied -= ShowGameEnded;
    }

    IEnumerator ScoreRoll()
    {
        int score = 0;
        while(score!=GameManager.Instance.score)
        {
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.005f));

            score += 1;
            txtEndScore.text = (score).ToString();
            
        }
        int intBestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (intBestScore < score)
        {
            //bestScore.text = lastScore.text;
            PlayerPrefs.SetInt("BestScore", score);
        }
        txtHightScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        yield return null;
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