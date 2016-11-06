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

        GameManager.Instance.ResetAll();
    }
    void Start()
    {
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
        txtEndScore.text = GameManager.Instance.score.ToString();
        CalculateScore();
        GameEnded.SetActive(true);
    }
    void OnDisable()
    {
        GameManager.PlayerDied -= ShowGameEnded;
    }
     void CalculateScore()
    {
        int GMscore = GameManager.Instance.score;
        txtScore.text = GMscore.ToString();

        int intBestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (intBestScore < GMscore)
        {
            //bestScore.text = lastScore.text;
            PlayerPrefs.SetInt("BestScore", GMscore);
        }
        txtHightScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }
}
