using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using GooglePlayGames;

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
public delegate void GameStarted();
#endregion

public class GameManager : MonoBehaviour
{
    public static event PlayerDied PlayerDied;
    public static event PlayerDied NewWave;
    public static event GameStarted NewGame;

    public static GameManager Instance;
    public PoolManager Pool_Manager;

    public List<GameObject> Items=new List<GameObject>();
    public List<GameObject> Weapons = new List<GameObject>();
    public List<GameObject> MainWeaponLvls = new List<GameObject>();
    public List<GameObject> TempWeapons = new List<GameObject>();
    public List<GameObject> TempWeaponLvls = new List<GameObject>();
    public List<EnergyBoost> EnergyBoosts = new List<EnergyBoost>();
    public List<FireRateBoost> FireRateBoosts = new List<FireRateBoost>();
    public List<DamageBoost> DamageBoosts = new List<DamageBoost>();
    public List<ShieldBoost> ShieldBoosts = new List<ShieldBoost>();
    List<Boost> ActiveBoosts = new List<Boost>();
    
    [HideInInspector]
    public GameStates currentGameStates;

    [HideInInspector]
    public GameScenes currentGameScene;

    [HideInInspector]
    public int currentScene;

    [HideInInspector]
    public int currentState;
    public bool Show_FPS = true;
    float deltaTime = 0.0f;
    public int weaponCoolDown = 20;
    public bool IsconnectedToGoogleServices = false;

    public int CurrentWaveNumber = 0;//
    public int Currentlevel = 1;//
    public bool IsAttacking = false;
 
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
        UI.Pause += PauseGame;
        UI.Resume += ResumeGame;
    }
    void Start()
    {
#if UNITY_EDITOR
#else
        PlayGamesPlatform.Activate();
        ConnectToGooglePlayServices();
#endif
        }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime);

    }
    private static void OnPlayerDies()
    {
        var handler = PlayerDied;
        if (handler != null) handler();
    }
    private static void OnGameStarted()
    {
        var handler = NewGame;
        if (handler != null) handler();
    }
    private static void OnNewWave()
    {
        var handler = NewWave;
        if (handler != null) handler();
    }
    public GameObject GetMainWeapon()
    {
        return Weapons[DataHandler.Instance.GetMainWeaponID()];
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
        Enemies_Manager.Instance.ResetWaveAndLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame(0, 1);
        DataHandler.Instance.ResetPlayerInGameData();
       
    }
    public void ReturnToMainMenu()
    {
        StopCoroutine("IncreaseEnergy");
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToLevelSelect()
    {
        StopCoroutine("IncreaseEnergy");
        SceneManager.LoadScene("Level Select");
    }
    public void StartGame(int Wave,int Level)
    {
        //Enemies_Manager.Instance.ResetWaveAndLevel();
        CurrentWaveNumber = Wave;
        Currentlevel = Level;
      
        SceneManager.LoadScene(2);
        
        if (IsconnectedToGoogleServices)
        {
            //Games.Achievements.unlock(mGoogleApiClient, "my_achievement_id");
            UnlockAchievement(SurvivalCubeResources.achievement_the_adventure_begins,  100);
        }
        //DataHandler.Instance.ResetPlayerInGameData();

        StartCoroutine("IncreaseEnergy");
        //UnlockAchievement1();
    }
    
    public void UnlockAchievement(string achievement, int coins, int increment)
    {
        if (IsconnectedToGoogleServices)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(achievement, increment, (bool success) =>
            {
                if (success)
                {
                    DataHandler.Instance.AddCoins(coins);

                }
                // handle success or failure
            });
        }
    }
    public void UnlockAchievement(string achievement, int coins)
    {
        if (IsconnectedToGoogleServices)
        {
            Social.ReportProgress(achievement, 100.0f, (bool success) =>
            {
                if (success)
                {
                    DataHandler.Instance.AddCoins(coins);
                }
                //    _success = success;
                //    // handle success or failure
            });
        }
    }
    public void StartGame()
    {
        //Enemies_Manager.Instance.ResetWaveAndLevel();
        SceneManager.LoadScene("Game");

        if (IsconnectedToGoogleServices)
        {
            //Games.Achievements.unlock(mGoogleApiClient, "my_achievement_id");

            UnlockAchievement(SurvivalCubeResources.achievement_the_adventure_begins, 100);

        }
        DataHandler.Instance.ResetPlayerInGameData();

        StartCoroutine("IncreaseEnergy");
        //UnlockAchievement1();
    }
    void PauseGame()
    {
        StopCoroutine("IncreaseEnergy");
    }
    void ResumeGame()
    {
        StartCoroutine("IncreaseEnergy");
    }
    public void ThePlayerDied()
    {
        StopCoroutine("IncreaseEnergy");
        DataHandler.Instance.AddPlayerDeath();
        GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_getting_tired_of__resurrecting_you, 100, 1);
        OnPlayerDies();
    }
    public void NewWavStarted()
    {
        DataHandler.Instance.AddEnergy(10);   
        OnNewWave();
    }
    public ObjectPool CreatePool(GameObject poolObject, int size, int maxSize)
    {
        return Pool_Manager.CreatePool(poolObject, size, maxSize);
    }
    void OnGUI()
    {
        if (Show_FPS)
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 4 / 100;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
    public void AddToActiveBoosts(Boost boost)
    {
        if (boost.CheckIfAllowed())
        {
            boost.Activate();
            ActiveBoosts.Add(boost);
        }
    }
    public void DeactivateAllBoosts()
    {
        foreach  (Boost boost in ActiveBoosts)
        {
            boost.Deactivate();
        }
        ActiveBoosts.Clear();
    }
    public bool ConnectToGooglePlayServices()
    {
        if (!IsconnectedToGoogleServices)
        {
            Social.localUser.Authenticate((bool Success) => {
                IsconnectedToGoogleServices = Success;
            });
        }
        return IsconnectedToGoogleServices;
    }
  
    public bool ReportScoreToLeaderBoard(int Score)
    {
        bool _success = false;
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore(Score, SurvivalCubeResources.leaderboard_topplayers, (bool success) => {
            _success = success;
            // handle success or failure
        });
        return _success;
    }
    IEnumerator IncreaseEnergy()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(DataHandler.Instance.GetPlayerEnergyRate());
            DataHandler.Instance.AddEnergy(5);
        }
    }
    private void OnDestroy()
    {
        UI.Pause -= PauseGame;
        UI.Resume -= ResumeGame;
    }
    public void UpgradeMainWeapon(int index)
    {
        if (DataHandler.Instance.GetMainWeaponLevel(index) < 2)
        {
            DataHandler.Instance.UpgradeMainWeapon(index);
            int level = DataHandler.Instance.GetMainWeaponLevel(index);
            Weapons[index] = MainWeaponLvls[level + (index * 3)];
        }
    }
    public void UpgradeTempWeapon(int index)
    {
        if (DataHandler.Instance.GetTempWeaponLevel(index) < 2)
        {
            DataHandler.Instance.UpgradeTempWeapon(index);
            int level = DataHandler.Instance.GetTempWeaponLevel(index);
            TempWeapons[index] = TempWeaponLvls[level + (index * 3)];
        }
    }
}
