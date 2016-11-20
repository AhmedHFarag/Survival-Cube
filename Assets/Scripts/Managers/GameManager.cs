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
    public static event PlayerDied NewWave;

    public static GameManager Instance;
    public PoolManager Pool_Manager;

    public List<GameObject> Items=new List<GameObject>();
    public List<GameObject> Weapons = new List<GameObject>();
    public List<GameObject> TempWeapons = new List<GameObject>();
    [HideInInspector]
    public GameStates currentGameStates;

    [HideInInspector]
    public GameScenes currentGameScene;

    [HideInInspector]
    public int currentScene;

    [HideInInspector]
    public int currentState;

    public int weaponCoolDown = 20;
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
    private static void OnNewWave()
    {
        var handler = NewWave;
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
                  //  PlayerPrefs.SetInt("AcivementScore", DataHandler.Instance.AcivementScore);
                 //   PlayerPrefs.SetInt("playerCoins", DataHandler.Instance.playerCoins);
                 //   PlayerPrefs.SetInt("inGameCoins", DataHandler.Instance.inGameCoins);
                 //   PlayerPrefs.Save();
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
     //   score = 0;
        //InGameCoins = 0;
      //  Coins = PlayerPrefs.GetInt("Coins", 0);
    }
    public void SpawnItem(Vector3 _pos)
    {
        if (!Player_Controller.Instance.Buffed)
        {
            if (Random.Range(0, 10) == 2)
            {
                GameObject obj = Instantiate(Items[Random.Range(0, Items.Count)]);
                obj.transform.position = _pos + new Vector3(0, 5, 0);
            }
        }
    }
    public void ReloadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //DataHandler.Instance.ResetPlayerPtrefData();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        DataHandler.Instance.ResetPlayerPtrefData();
    }
    public void ThePlayerDied()
    {
        OnPlayerDies();
    }
    public void NewWavStarted()
    {
        OnNewWave();
    }
    public ObjectPool CreatePool(GameObject poolObject, int size, int maxSize)
    {
        return Pool_Manager.CreatePool(poolObject, size, maxSize);
    }
}
