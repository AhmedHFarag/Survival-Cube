using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public static UI Instance;

    public GameObject GameEnded;
    public Text txtscore;

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
    }
    void Start()
    {
        GameManager.PlayerDied += ShowGameEnded;
        Enemy.OnEnemyDie += EnemyDeath;
    }

    void Update()
    {

    }
    public void ReStartGame()
    {
        GameManager.Instance.ReloadSameScene();
    }
    public void ShowGameEnded()
    {
        Time.timeScale = 0;
        GameEnded.SetActive(true);
    }
    void OnDisable()
    {
        GameManager.PlayerDied -= ShowGameEnded;
        Enemy.OnEnemyDie -= EnemyDeath;
    }

    void EnemyDeath()
    {
        txtscore.text = GameManager.Instance.score.ToString();
    }
}
