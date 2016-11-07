using UnityEngine;
using System.Collections;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;

    public string PlayerName;
    public int BG_Volume;
    public int SFX_Volume;
    public int playerCoins;
    public int AcivementScore;
    public int WaveNo = 0;

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

    void initializeData()
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
            WaveNo = 0;
        }

        //Saving 
        PlayerPrefs.Save();

    }
    public void SavePlayerPrefsData()
    {
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.SetString("playerName", PlayerName);
        PlayerPrefs.SetInt("bgVolume", BG_Volume);
        PlayerPrefs.SetInt("sfxVolume", SFX_Volume);
        PlayerPrefs.SetInt("WaveNo", WaveNo);
        PlayerPrefs.Save();
    }
    void AddCoins(int amountToBeAdded)
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
