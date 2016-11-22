using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour
{

    struct WeaponsData
    {
      public int WeaponID;
      public int WeaponStatus;
      public int WeaponCost;
      public int WeaponDamage;
    }
    struct WaveData
    {
        public int WaveNumber;

    }
    struct PlayerData
    {
        public string Name;
        public int Coins;
        public int HighScore;
        public int MaxWaveReached;
        public int HighestWaveStreak;
    }
    PlayerData Player;
    WaveData Wave;
    string MaxWaveReachedRef = "MaxWaveReached";
    string HighestWaveStreakRef = "HighestWaveStreak";
    WeaponsData weapon1;
    WeaponsData weapon2;
    WeaponsData weapon3;

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
    public int AchievementScore;
    [HideInInspector]
    public int BestScore;
    [HideInInspector]
    
    

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
            Player.Coins = PlayerPrefs.GetInt("playerCoins");
        }
        else
        {
            PlayerPrefs.SetInt("playerCoins", 0);
            Player.Coins = 0;
        }
        
        //Player Name
        if (PlayerPrefs.HasKey("playerName"))
        {
            Player.Name = PlayerPrefs.GetString("playerName");
        }
        else
        {
            PlayerPrefs.SetString("playerName", "New Player");
            Player.Name = "New Player";
        }

        if (PlayerPrefs.HasKey("BestScore"))
        {
            Player.HighScore = PlayerPrefs.GetInt("BestScore");
            
        }
        else
        {
            PlayerPrefs.SetInt("BestSCore", 0);
            Player.HighScore = 0;
        }

        if (PlayerPrefs.HasKey("AcivementScore"))
        {
            AchievementScore = PlayerPrefs.GetInt("AcivementScore");
        }
        else
        {
            PlayerPrefs.SetInt("AcivementScore", 0);
            AchievementScore = 0;
        }

        if (PlayerPrefs.HasKey("weapon1.WeaponID"))
        {
            weapon1.WeaponID = PlayerPrefs.GetInt("weapon1.WeaponID");
        }
        else
        {
            PlayerPrefs.SetInt("weapon1.WeaponID", 1);
            weapon1.WeaponID = 1;
        }
        if (PlayerPrefs.HasKey("weapon1.WeaponStatus"))
        {
            weapon1.WeaponStatus = PlayerPrefs.GetInt("weapon1.WeaponStatus");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon1.WeaponStatus", 5);
            weapon1.WeaponStatus = 5;
        }
        if(PlayerPrefs.HasKey("weapon1.WeaponCost"))
        {
            weapon1.WeaponCost = PlayerPrefs.GetInt("weapon1.WeaponCost");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon1.WeaponCost", 30);
            weapon1.WeaponCost = 30;
        }
        if (PlayerPrefs.HasKey("weapon1.WeaponDamage"))
        {
            weapon1.WeaponDamage = PlayerPrefs.GetInt("weapon1.WeaponDamage");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon1.WeaponDamage", 2);
            weapon1.WeaponDamage = 2;
        }
        if (PlayerPrefs.HasKey("weapon2.WeaponID"))
        {
            weapon2.WeaponID = PlayerPrefs.GetInt("weapon2.WeaponID");
        }
        else
        {
            PlayerPrefs.SetInt("weapon2.WeaponID", 2);
            weapon2.WeaponID = 2;
        }
        if (PlayerPrefs.HasKey("weapon2.WeaponStatus"))
        {
            weapon2.WeaponStatus = PlayerPrefs.GetInt("weapon2.WeaponStatus");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon2.WeaponStatus", 5);
            weapon2.WeaponStatus = 5;
        }
        if (PlayerPrefs.HasKey("weapon2.WeaponCost"))
        {
            weapon2.WeaponCost = PlayerPrefs.GetInt("weapon2.WeaponCost");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon2.WeaponCost", 30);
            weapon2.WeaponCost = 30;
        }
        if (PlayerPrefs.HasKey("weapon2.WeaponDamage"))
        {
            weapon2.WeaponDamage = PlayerPrefs.GetInt("weapon2.WeaponDamage");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon2.WeaponDamage", 3);
            weapon2.WeaponDamage = 3;
        }

        if (PlayerPrefs.HasKey("weapon3.WeaponID"))
        {
            weapon3.WeaponID = PlayerPrefs.GetInt("weapon3.WeaponID");
        }
        else
        {
            PlayerPrefs.SetInt("weapon3.WeaponID", 3);
            weapon3.WeaponID = 3;
        }
        if (PlayerPrefs.HasKey("weapon3.WeaponStatus"))
        {
            weapon3.WeaponStatus = PlayerPrefs.GetInt("weapon3.WeaponStatus");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon3.WeaponStatus", 5);    
            weapon3.WeaponStatus = 5;
        }
        if (PlayerPrefs.HasKey("weapon3.WeaponCost"))
        {
            weapon3.WeaponCost = PlayerPrefs.GetInt("weapon3.WeaponCost");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon3.WeaponCost", 30);
            weapon3.WeaponCost = 30;
        }
        if (PlayerPrefs.HasKey("weapon3.WeaponDamage"))
        {
            weapon3.WeaponDamage = PlayerPrefs.GetInt("weapon3.WeaponDamage");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon3.WeaponDamage", 4);
            weapon1.WeaponDamage = 4;
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
            Wave.WaveNumber = PlayerPrefs.GetInt("WaveNo");
        }
        else
        {
            Wave.WaveNumber = 1;
            PlayerPrefs.SetInt("WaveNo", Wave.WaveNumber);
            
        }
        if (PlayerPrefs.HasKey(MaxWaveReachedRef))
        {
            Player.MaxWaveReached = PlayerPrefs.GetInt(MaxWaveReachedRef);
        }
        else
        {
            PlayerPrefs.SetInt(MaxWaveReachedRef, Player.MaxWaveReached);
        }
        if(PlayerPrefs.HasKey(HighestWaveStreakRef))
        {
            Player.HighestWaveStreak = PlayerPrefs.GetInt(HighestWaveStreakRef);
        }
        else
        {
            PlayerPrefs.SetInt(HighestWaveStreakRef, Player.HighestWaveStreak);
        }

        //Saving 
        PlayerPrefs.Save();

    }
    public void ResetPlayerPtrefData()
    {
        inGameCoins = 0;
        AchievementScore = 0;
        Player.MaxWaveReached = 0;
        Player.HighestWaveStreak = 0;
        playerCoins = 0;
        PlayerName = "";
        PlayerPrefs.DeleteAll();
        initializeData();

    }
    public void SavePlayerPrefsData()
    {
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.SetInt("inGameCoins", inGameCoins);
        PlayerPrefs.SetString("playerName", PlayerName);
        PlayerPrefs.SetInt("bgVolume", BG_Volume);
        PlayerPrefs.SetInt("sfxVolume", SFX_Volume);
        PlayerPrefs.SetInt("WaveNo", Wave.WaveNumber);
        PlayerPrefs.SetInt("AcivementScore", AchievementScore);
        PlayerPrefs.SetInt("BestScore", BestScore);

        PlayerPrefs.SetInt("weapon1.WeaponID", weapon1.WeaponID);
        PlayerPrefs.SetInt("weapon1.WeaponStatus", weapon1.WeaponStatus);
        PlayerPrefs.SetInt("weapon1.WeaponCost", weapon1.WeaponCost);
        PlayerPrefs.SetInt("weapon1.WeaponDamage", weapon1.WeaponDamage);

        PlayerPrefs.SetInt("weapon2.WeaponID", weapon2.WeaponID);
        PlayerPrefs.SetInt("weapon2.WeaponStatus", weapon2.WeaponStatus);
        PlayerPrefs.SetInt("weapon2.WeaponCost", weapon2.WeaponCost);
        PlayerPrefs.SetInt("weapon2.WeaponDamage", weapon2.WeaponDamage);

        PlayerPrefs.SetInt("weapon3.WeaponID", weapon3.WeaponID);
        PlayerPrefs.SetInt("weapon3.WeaponStatus", weapon3.WeaponStatus);
        PlayerPrefs.SetInt("weapon3.WeaponCost", weapon3.WeaponCost);
        PlayerPrefs.SetInt("weapon3.WeaponDamage", weapon3.WeaponDamage);

        PlayerPrefs.Save();
    }
    #region Getters
    public int GetPlayerCoins()
    {
        return Player.Coins;
    }
    public string GetPlayerCoinsstr()
    {
        return Player.Coins.ToString();
    }

    public string GetPlayerName()
    {
        return Player.Name;
    }

    public int  GetWaveNumber()
    {
        return Wave.WaveNumber;
    }
    public int GetMaxWaveReached()
    {
        return Player.MaxWaveReached;
    }
    public int GetHighestWaveStreak()
    {
        return Player.HighestWaveStreak;
    }
    public int GetBestScore()
    {
        return Player.HighScore;
    }
    public string GetBestScoreStr()
    {
        return Player.HighScore.ToString();
    }
    public int GetAcivementScore()
    {
        return AchievementScore;
    }
    public int GetWeapon1ID()
    {
        return weapon1.WeaponID;
    }
    public int GetWeapon1Status()
    {
        return weapon1.WeaponStatus;
    }
    public int GetWeapon1Cost()
    {
        return weapon1.WeaponCost;
    }
    public int GetWeapon1Damage()
    {
        return weapon1.WeaponDamage;
    }

    public int GetWeapon2ID()
    {
        return weapon2.WeaponID;
    }
    public int GetWeapon2Status()
    {
        return weapon2.WeaponStatus;
    }
    public int GetWeapon2Cost()
    {
        return weapon2.WeaponCost;
    }
    public int GetWeapon2Damage()
    {
        return weapon2.WeaponDamage;
    }
    public int GetWeapon3ID()
    {
        return weapon3.WeaponID;
    }
    public int GetWeapon3Status()
    {
        return weapon3.WeaponStatus;
    }
    public int GetWeapon3Cost()
    {
        return weapon3.WeaponCost;
    }
    public int GetWeapon3Damage()
    {
        return weapon3.WeaponDamage;
    }
    #endregion
    #region Setters
    public void SetPlayerCoins(int _PlayerCoins)
    {
        playerCoins = _PlayerCoins;
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.Save();
    }
    public void SetInGameCoins(int _InGameCoins)
    {
        inGameCoins = _InGameCoins;
    }
    public void SetPlayerName(string _PlayerName)
    {
        Player.Name = _PlayerName;
        PlayerPrefs.SetString("playerName", _PlayerName);

        PlayerPrefs.Save();
    }

    public void SetWaveNumber(int _WaveNo)
    {   
        Wave.WaveNumber = _WaveNo;
        
    }
    public void SetMaxWaveReached(int _WaveNumber)
    {
        Player.MaxWaveReached = _WaveNumber;
        PlayerPrefs.SetInt(MaxWaveReachedRef, _WaveNumber);
        PlayerPrefs.Save();
    }
    public void SetHighestWaveStreak(int _Waves)
    {
        Player.HighestWaveStreak = _Waves;
        PlayerPrefs.SetInt(HighestWaveStreakRef, _Waves);
        PlayerPrefs.Save();
    }
    public void SetBestScore(int _BestScore)
    {
        Player.HighScore = _BestScore;
        PlayerPrefs.SetInt("BestScore", _BestScore);
        PlayerPrefs.Save();
    }
    public void SetAcivementScore(int _AchievementScore)
    {
        AchievementScore = _AchievementScore;
        PlayerPrefs.SetInt("AcivementScore", _AchievementScore);
        PlayerPrefs.Save();
    }



    public void SetWeapon1ID(int _weaponID)
    {
        weapon1.WeaponID = _weaponID;
        SavePlayerPrefsData();
    }
    public void SetWeapon1Status(int _weaponStatus)
    {
        weapon1.WeaponStatus = _weaponStatus;
        SavePlayerPrefsData();
    }
    public void SetWeapon1Cost(int _weaponCost)
    {
        weapon1.WeaponCost = _weaponCost;
        SavePlayerPrefsData();
    }
    public void SetWeapon1Damage(int _weaponDamage)
    {
        weapon1.WeaponDamage = _weaponDamage;
        SavePlayerPrefsData();
    }

    public void SetWeapon2ID(int _weaponID)
    {
        weapon2.WeaponID = _weaponID;
        SavePlayerPrefsData();
    }
    public void SetWeapon2Status(int _weaponStatus)
    {
        weapon2.WeaponStatus = _weaponStatus;
        SavePlayerPrefsData();
    }
    public void SetWeapon2Cost(int _weaponCost)
    {
        weapon2.WeaponCost = _weaponCost;
        SavePlayerPrefsData();
    }
    public void SetWeapon2Damage(int _weaponDamage)
    {
        weapon2.WeaponDamage = _weaponDamage;
        SavePlayerPrefsData();
    }

    public void SetWeapon3ID(int _weaponID)
    {
        weapon3.WeaponID = _weaponID;
        SavePlayerPrefsData();
    }
    public void SetWeapon3Status(int _weaponStatus)
    {
        weapon3.WeaponStatus = _weaponStatus;
        SavePlayerPrefsData();
    }
    public void SetWeapon3Cost(int _weaponCost)
    {
        weapon3.WeaponCost = _weaponCost;
        SavePlayerPrefsData();
    }
    public void SetWeapon3Damage(int _weaponDamage)
    {
        weapon3.WeaponDamage = _weaponDamage;
        SavePlayerPrefsData();
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
