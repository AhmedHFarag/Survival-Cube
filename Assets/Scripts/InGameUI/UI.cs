using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public static UI Instance;

    public GameObject GameEnded;
    public Text txtScore;
    public Text txtEndScore;

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

        Enemy.OnEnemyDie += EnemyDeath;
        GameManager.Instance.ResetAll();
    }
    void Start()
    {
        GameManager.PlayerDied += ShowGameEnded;
    }

    void Update()
    {

    }
    public void ReStartGame()
    {
        Debug.Log("is working");
        Time.timeScale = 1;
        GameManager.Instance.ReloadSameScene();

    }
    public void ShowGameEnded()
    {
        Time.timeScale = 0;
        txtEndScore.text = GameManager.Instance.score.ToString();
        GameEnded.SetActive(true);
    }
    void OnDisable()
    {
        GameManager.PlayerDied -= ShowGameEnded;
        Enemy.OnEnemyDie -= EnemyDeath;
    }

    void EnemyDeath()
    {
        txtScore.text = GameManager.Instance.score.ToString();
    }
}
