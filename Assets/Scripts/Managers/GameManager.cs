using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Item;
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

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

    private void ResetAll()
    {
        score = 0;
    }
    public void SpawnItem(Vector3 _pos)
    {
        if (Random.Range(0,4)==2)
        {
            GameObject obj = Instantiate(Item);
            obj.transform.position = _pos+new Vector3(0,5,0);
        }
    }
    public void ReloadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
