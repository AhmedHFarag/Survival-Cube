using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameStates
{
    GameStarted,
    InGamePlay,
    GamePause,
    GameResume,
    GameEnded
}

public enum GameScenes
{
    Splash,
    Tutorial,
    InGame
}
#region Declaring delegate events
public delegate void PlayerDied();//Declaring Die event delegate
#endregion

public class GameManager : MonoBehaviour
{
    public static event PlayerDied PlayerDied;

    public static GameManager Instance;
    public PoolManager Pool_Manager;

    public List<GameObject> Items=new List<GameObject>();
    [HideInInspector]
    public GameStates currentGameStates;

    [HideInInspector]
    public GameScenes currentGameScene;

    [HideInInspector]
    public int currentScene;

    [HideInInspector]
    public int currentState;

    [HideInInspector]
    public int score;


    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        Pool_Manager = new PoolManager();
    }
    private static void OnPlayerDies()
    {
        var handler = PlayerDied;
        if (handler != null) handler();
    }
    public void SwitchGameStates()
    {
        switch (currentGameStates)
        {
            case GameStates.GameStarted:
                {
                    Time.timeScale = 1;
                    break;
                }
            case GameStates.InGamePlay:
                {
                    Time.timeScale = 1;
                    break;
                }
            case GameStates.GamePause:
                {
                    Time.timeScale = 0;
                    break;
                }
            case GameStates.GameResume:
                {
                    Time.timeScale = 1;
                    break;
                }
            case GameStates.GameEnded:
                {
                    //calculate the score :)
                    Time.timeScale = 0;
                    break;
                }
        }
    }

    public void SwitchGameScenes()
    {
        switch (currentGameScene)
        {
            case GameScenes.Splash:
                {
                    currentScene = (int)GameScenes.Splash;
                    SceneManager.LoadScene((int)GameScenes.Tutorial, LoadSceneMode.Single);
                    break;
                }

            case GameScenes.Tutorial:
                {
                    currentScene = (int)GameScenes.Tutorial;
                    SceneManager.LoadScene((int)GameScenes.Tutorial, LoadSceneMode.Single);
                    break;
                }

            case GameScenes.InGame:
                {
                    ResetAll();
                    currentScene = (int)GameScenes.Tutorial;
                    SceneManager.LoadScene((int)GameScenes.Tutorial, LoadSceneMode.Single);
                    break;
                }
        }
        //StartCoroutine(Delay());
    }

    public void ResetAll()
    {
        Pool_Manager = new PoolManager();
        score = 0;
    }
    public void SpawnItem(Vector3 _pos)
    {
        if (!Player_Controller.Instance.Buffed)
        {
            if (Random.Range(0, 4) == 2)
            {
                GameObject obj = Instantiate(Items[Random.Range(0, Items.Count)]);
                obj.transform.position = _pos + new Vector3(0, 5, 0);
            }
        }
    }
    public void ReloadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ThePlayerDied()
    {
        OnPlayerDies();
    }
    public ObjectPool CreatePool(GameObject poolObject, int size, int maxSize)
    {
        return Pool_Manager.CreatePool(poolObject, size, maxSize);
    }
}
