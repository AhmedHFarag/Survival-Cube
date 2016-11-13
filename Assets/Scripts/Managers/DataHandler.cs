using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;

    [HideInInspector]
    public string PlayerName;
    [HideInInspector]
    public int BG_Volume;
    [HideInInspector]
    public int SFX_Volume;
    [HideInInspector]
    public int playerCoins;
    [HideInInspector]
    public int inGameCoins;
    [HideInInspector]
    public int AcivementScore;
    [HideInInspector]
    public int BestScore;
    [HideInInspector]
    public int WaveNo = 0;
    //   Dictionary<int,int> wapeonsSlotIdAndStatus = new Dictionary<int, int>();
    [HideInInspector]
    public int weapon1ID;
    [HideInInspector]
    public int weapon2ID;
    [HideInInspector]
    public int weapon3ID;
    [HideInInspector]
    public int weapon1upgradestatus;
    [HideInInspector]
    public int weapon2upgradestatus;
    [HideInInspector]
    public int weapon3upgradestatus;


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
        initializeData();
    }

   public void initializeData()
    {
        if (PlayerPrefs.HasKey("BetaRelease"))
        {

        }
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("BetaRelease", 1);
        }
        //Coins
        if (PlayerPrefs.HasKey("playerCoins"))
        {
            playerCoins = PlayerPrefs.GetInt("playerCoins");
        }
        else
        {
            PlayerPrefs.SetInt("playerCoins", 0);
            playerCoins = 0;
        }
        if (PlayerPrefs.HasKey("inGameCoins"))
        {
            inGameCoins = PlayerPrefs.GetInt("inGameCoins");
        }
        else
        {
            PlayerPrefs.SetInt("inGameCoins", 0);
            inGameCoins = 0;
        }
        //Player Name
        if (PlayerPrefs.HasKey("playerName"))
        {
            PlayerName = PlayerPrefs.GetString("playerName");
        }
        else
        {
            PlayerPrefs.SetString("playerName", "New Player");
            PlayerName = "New Player";
        }

        if (PlayerPrefs.HasKey("BestScore"))
        {
            BestScore = PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            PlayerPrefs.SetInt("BestSCore", 0);
            BestScore = 0;
        }

        if (PlayerPrefs.HasKey("AcivementScore"))
        {
            AcivementScore = PlayerPrefs.GetInt("AcivementScore");
        }
        else
        {
            PlayerPrefs.SetInt("AcivementScore", 0);
            AcivementScore = 0;
        }

        if (PlayerPrefs.HasKey("weapon1ID"))
        {
            weapon1ID = PlayerPrefs.GetInt("weapon1ID");
        }
        else
        {
            PlayerPrefs.SetInt("weapon1ID", 1);
            weapon1ID = 1;
        }
        if (PlayerPrefs.HasKey("weapon2ID"))
        {
            weapon2ID = PlayerPrefs.GetInt("weapon2ID");
        }
        else
        {
            PlayerPrefs.SetInt("weapon2ID", 2);
            weapon2ID = 2;
        }
        if (PlayerPrefs.HasKey("weapon3ID"))
        {
            weapon3ID = PlayerPrefs.GetInt("weapon3ID");
        }
        else
        {
            PlayerPrefs.SetInt("weapon3ID", 3);
            weapon3ID = 3;
        }

        if (PlayerPrefs.HasKey("weapon1upgradestatus"))
        {
            weapon1upgradestatus = PlayerPrefs.GetInt("weapon1upgradestatus");
        }
        else
        {
            PlayerPrefs.SetInt("weapon1upgradestatus", 5);
            weapon1upgradestatus = 5;
        }
        if (PlayerPrefs.HasKey("weapon2upgradestatus"))
        {
            weapon2upgradestatus = PlayerPrefs.GetInt("weapon2upgradestatus");
        }
        else
        {
            PlayerPrefs.SetInt("weapon2upgradestatus", 5);
            weapon2upgradestatus = 5;
        }
        if (PlayerPrefs.HasKey("weapon3upgradestatus"))
        {
            weapon3upgradestatus = PlayerPrefs.GetInt("weapon3upgradestatus");
        }
        else
        {
            PlayerPrefs.SetInt("weapon3upgradestatus", 5);
            weapon3upgradestatus = 5;
        }

        //Master Volume
        if (PlayerPrefs.HasKey("bgVolume"))
        {
            BG_Volume = PlayerPrefs.GetInt("bgVolume");
        }
        else
        {
            PlayerPrefs.SetInt("bgVolume", 100);
            BG_Volume = 100;
        }

        //Music Volume
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            SFX_Volume = PlayerPrefs.GetInt("sfxVolume");
        }
        else
        {
            PlayerPrefs.SetInt("sfxVolume", 50);
            SFX_Volume = 50;
        }


        //No Of waves Unlocked
        if (PlayerPrefs.HasKey("WaveNo"))
        {
            WaveNo = PlayerPrefs.GetInt("WaveNo");
        }
        else
        {
            PlayerPrefs.SetInt("WaveNo", WaveNo);
            WaveNo = 1;
        }


        //Saving 
        PlayerPrefs.Save();

    }
    public void ResetPlayerPtrefData()
    {
        PlayerPrefs.SetInt("AcivementScore", 0);
      //  PlayerPrefs.SetInt("WaveNo", 1);
        PlayerPrefs.SetInt("playerCoins", 0);
    }
    public void SavePlayerPrefsData()
    {
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.SetInt("inGameCoins", inGameCoins);
        PlayerPrefs.SetString("playerName", PlayerName);
        PlayerPrefs.SetInt("bgVolume", BG_Volume);
        PlayerPrefs.SetInt("sfxVolume", SFX_Volume);
        PlayerPrefs.SetInt("WaveNo", WaveNo);
        PlayerPrefs.SetInt("AcivementScore", AcivementScore);
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("weapon1ID", weapon1ID);
        PlayerPrefs.SetInt("weapon2ID", weapon2ID);
        PlayerPrefs.SetInt("weapon3ID", weapon3ID);
        PlayerPrefs.SetInt("weapon1upgradestatus", weapon1upgradestatus);
        PlayerPrefs.SetInt("weapon2upgradestatus", weapon2upgradestatus);
        PlayerPrefs.SetInt("weapon3upgradestatus", weapon3upgradestatus);
        PlayerPrefs.Save();
    }
    #region Getters
    public void GetPlayerCoins()
    {
        PlayerPrefs.GetInt("playerCoins", playerCoins);
    }

    public void GetPlayerName()
    {
        PlayerPrefs.SetString("PlayerName", PlayerName);
    }

    public void GetWaveNumber()
    {
        PlayerPrefs.GetInt("WaveNo", WaveNo);
    }
    public void GetBestScore()
    {
        PlayerPrefs.GetInt("BestScore", BestScore);
    }
    public void GetAcivementScore()
    {
        PlayerPrefs.GetInt("AcivementScore", AcivementScore);
    }
    #endregion

   public void AddCoins(int amountToBeAdded)
    {
        playerCoins += amountToBeAdded;
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.Save();
    }

    void RemoveCoins(int amountToBeRemoved)
    {
        playerCoins -= amountToBeRemoved;
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.Save();
    }
}
