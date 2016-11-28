using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour
{

    struct TempWeaponsData
    {
      public int WeaponID;
      public int WeaponStatus;
      public int WeaponCost;
      public int WeaponDamage;
    }
    public struct MainWeaponData
    {
       public int ID;
       public int Damage;
       public int Range;
       public int FireRate;
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

    MainWeaponData MainWeapon;
    TempWeaponsData[] Tempweapon=new TempWeaponsData[3];

    public static DataHandler Instance;
    [HideInInspector]
    string PlayerName;
    [HideInInspector]
    int BG_Volume;
    [HideInInspector]
    int SFX_Volume;
    [HideInInspector]
    int inGameCoins;
    [HideInInspector]
    int inGameScore;

    bool DataLoaded = false;
    
    

    void Awake()
    {
        DataLoaded = false;
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        initializeData();
        DataLoaded = true;
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

        //if (PlayerPrefs.HasKey("InGameScore"))
        //{
        //    inGameScore = PlayerPrefs.GetInt("InGameScore");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("InGameScore", 0);
        //    inGameScore = 0;
        //}

        //Palyer Main Weapon
        if (PlayerPrefs.HasKey("MainWeapon.ID"))
        {
            MainWeapon.ID = PlayerPrefs.GetInt("MainWeapon.ID");

        }
        else
        {
            PlayerPrefs.SetInt("MainWeapon.ID",0);
            MainWeapon.ID = 0;
        }

        //if (PlayerPrefs.HasKey("tempweapon1.WeaponID"))
        //{
        //    tempweapon1.WeaponID = PlayerPrefs.GetInt("tempweapon1.WeaponID");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("tempweapon1.WeaponID", 1);
        //    tempweapon1.WeaponID = 1;
        //}
        //if (PlayerPrefs.HasKey("tempweapon1.WeaponStatus"))
        //{
        //    tempweapon1.WeaponStatus = PlayerPrefs.GetInt("tempweapon1.WeaponStatus");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon1.WeaponStatus", 5);
        //    tempweapon1.WeaponStatus = 5;
        //}
        //if(PlayerPrefs.HasKey("tempweapon1.WeaponCost"))
        //{
        //    tempweapon1.WeaponCost = PlayerPrefs.GetInt("tempweapon1.WeaponCost");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon1.WeaponCost", 30);
        //    tempweapon1.WeaponCost = 30;
        //}
        //if (PlayerPrefs.HasKey("tempweapon1.WeaponDamage"))
        //{
        //    tempweapon1.WeaponDamage = PlayerPrefs.GetInt("tempweapon1.WeaponDamage");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon1.WeaponDamage", 2);
        //    tempweapon1.WeaponDamage = 2;
        //}
        //if (PlayerPrefs.HasKey("tempweapon2.WeaponID"))
        //{
        //    tempweapon2.WeaponID = PlayerPrefs.GetInt("tempweapon2.WeaponID");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("tempweapon2.WeaponID", 2);
        //    tempweapon2.WeaponID = 2;
        //}
        //if (PlayerPrefs.HasKey("tempweapon2.WeaponStatus"))
        //{
        //    tempweapon2.WeaponStatus = PlayerPrefs.GetInt("tempweapon2.WeaponStatus");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon2.WeaponStatus", 5);
        //    tempweapon2.WeaponStatus = 5;
        //}
        //if (PlayerPrefs.HasKey("tempweapon2.WeaponCost"))
        //{
        //    tempweapon2.WeaponCost = PlayerPrefs.GetInt("tempweapon2.WeaponCost");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon2.WeaponCost", 30);
        //    tempweapon2.WeaponCost = 30;
        //}
        //if (PlayerPrefs.HasKey("tempweapon2.WeaponDamage"))
        //{
        //    tempweapon2.WeaponDamage = PlayerPrefs.GetInt("tempweapon2.WeaponDamage");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon2.WeaponDamage", 3);
        //    tempweapon2.WeaponDamage = 3;
        //}

        //if (PlayerPrefs.HasKey("tempweapon3.WeaponID"))
        //{
        //    tempweapon3.WeaponID = PlayerPrefs.GetInt("tempweapon3.WeaponID");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("tempweapon3.WeaponID", 3);
        //    tempweapon3.WeaponID = 3;
        //}
        //if (PlayerPrefs.HasKey("tempweapon3.WeaponStatus"))
        //{
        //    tempweapon3.WeaponStatus = PlayerPrefs.GetInt("tempweapon3.WeaponStatus");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon3.WeaponStatus", 5);    
        //    tempweapon3.WeaponStatus = 5;
        //}
        //if (PlayerPrefs.HasKey("tempweapon3.WeaponCost"))
        //{
        //    tempweapon3.WeaponCost = PlayerPrefs.GetInt("tempweapon3.WeaponCost");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon3.WeaponCost", 30);
        //    tempweapon3.WeaponCost = 30;
        //}
        //if (PlayerPrefs.HasKey("tempweapon3.WeaponDamage"))
        //{
        //    tempweapon3.WeaponDamage = PlayerPrefs.GetInt("tempweapon3.WeaponDamage");
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("tempweapon3.WeaponDamage", 4);
        //    tempweapon1.WeaponDamage = 4;
        //}
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
        inGameScore = 0;
        //Player.MaxWaveReached = 0;
        //Player.HighestWaveStreak = 0;
        //playerCoins = 0;
        //PlayerName = "";
        //PlayerPrefs.DeleteAll();
        //initializeData();

    }
    public void SavePlayerPrefsData()
    {
        PlayerPrefs.SetInt("playerCoins", Player.Coins);
        PlayerPrefs.SetInt("inGameCoins", inGameCoins);
        PlayerPrefs.SetString("playerName", PlayerName);
        PlayerPrefs.SetInt("bgVolume", BG_Volume);
        PlayerPrefs.SetInt("sfxVolume", SFX_Volume);
        PlayerPrefs.SetInt("WaveNo", Wave.WaveNumber);
        //PlayerPrefs.SetInt("InGameScore", inGameScore);
        PlayerPrefs.SetInt("BestScore", Player.HighScore);

        PlayerPrefs.SetInt("MainWeapon.ID", MainWeapon.ID);

        //PlayerPrefs.SetInt("weapon1.WeaponID", tempweapon1.WeaponID);
        //PlayerPrefs.SetInt("weapon1.WeaponStatus", tempweapon1.WeaponStatus);
        //PlayerPrefs.SetInt("weapon1.WeaponCost", tempweapon1.WeaponCost);
        //PlayerPrefs.SetInt("weapon1.WeaponDamage", tempweapon1.WeaponDamage);

        //PlayerPrefs.SetInt("weapon2.WeaponID", tempweapon2.WeaponID);
        //PlayerPrefs.SetInt("weapon2.WeaponStatus", tempweapon2.WeaponStatus);
        //PlayerPrefs.SetInt("weapon2.WeaponCost", tempweapon2.WeaponCost);
        //PlayerPrefs.SetInt("weapon2.WeaponDamage", tempweapon2.WeaponDamage);

        //PlayerPrefs.SetInt("weapon3.WeaponID", tempweapon3.WeaponID);
        //PlayerPrefs.SetInt("weapon3.WeaponStatus", tempweapon3.WeaponStatus);
        //PlayerPrefs.SetInt("weapon3.WeaponCost", tempweapon3.WeaponCost);
        //PlayerPrefs.SetInt("weapon3.WeaponDamage", tempweapon3.WeaponDamage);

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
    public int GetInGameCoins()
    {
        return inGameCoins;
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
    public int GetInGameScore()
    {
        return inGameScore;
    }
    public int GetMainWeaponID()
    {
        return MainWeapon.ID;
    }
    public int GetTempWeapon(int index)
    {
        return Tempweapon[index].WeaponID;
    }
    //public int GetTempWeapon1ID()
    //{
    //    return tempweapon1.WeaponID;
    //}
    //public int GetTempWeapon1Status()
    //{
    //    return tempweapon1.WeaponStatus;
    //}
    //public int GetTempWeapon1Cost()
    //{
    //    return tempweapon1.WeaponCost;
    //}
    //public int GetTempWeapon1Damage()
    //{
    //    return tempweapon1.WeaponDamage;
    //}

    //public int GetTempWeapon2ID()
    //{
    //    return tempweapon2.WeaponID;
    //}
    //public int GetTempWeapon2Status()
    //{
    //    return tempweapon2.WeaponStatus;
    //}
    //public int GetTempWeapon2Cost()
    //{
    //    return tempweapon2.WeaponCost;
    //}
    //public int GetTempWeapon2Damage()
    //{
    //    return tempweapon2.WeaponDamage;
    //}
    //public int GetTempWeapon3ID()
    //{
    //    return tempweapon3.WeaponID;
    //}
    //public int GetTempWeapon3Status()
    //{
    //    return tempweapon3.WeaponStatus;
    //}
    //public int GetTempWeapon3Cost()
    //{
    //    return tempweapon3.WeaponCost;
    //}
    //public int GetTempWeapon3Damage()
    //{
    //    return tempweapon3.WeaponDamage;
    //}
    #endregion
    #region Setters
    public void SetPlayerCoins(int _PlayerCoins)
    {
        Player.Coins = _PlayerCoins;
        PlayerPrefs.SetInt("playerCoins", Player.Coins);
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
    //public void SetInGameScore(int _AchievementScore)
    //{
    //    inGameScore = _AchievementScore;
    //    PlayerPrefs.SetInt("InGameScore", _AchievementScore);
    //    PlayerPrefs.Save();
    //}

    public void SteMainWeaponID(int _weaponID)
    {
        MainWeapon.ID = _weaponID;
        SavePlayerPrefsData();
    }

    public void SetTempWeapon(int index,int _weaponID)
    {
        Tempweapon[index].WeaponID = _weaponID;
    }
    //public void SetTempWeapon1ID(int _weaponID)
    //{
    //    tempweapon1.WeaponID = _weaponID;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon1Status(int _weaponStatus)
    //{
    //    tempweapon1.WeaponStatus = _weaponStatus;
    //    SavePlayerPrefsData();
    //}
    //public void SeTemptWeapon1Cost(int _weaponCost)
    //{
    //    tempweapon1.WeaponCost = _weaponCost;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon1Damage(int _weaponDamage)
    //{
    //    tempweapon1.WeaponDamage = _weaponDamage;
    //    SavePlayerPrefsData();
    //}

    //public void SetTempWeapon2ID(int _weaponID)
    //{
    //    tempweapon2.WeaponID = _weaponID;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon2Status(int _weaponStatus)
    //{
    //    tempweapon2.WeaponStatus = _weaponStatus;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon2Cost(int _weaponCost)
    //{
    //    tempweapon2.WeaponCost = _weaponCost;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon2Damage(int _weaponDamage)
    //{
    //    tempweapon2.WeaponDamage = _weaponDamage;
    //    SavePlayerPrefsData();
    //}

    //public void SetTempWeapon3ID(int _weaponID)
    //{
    //    tempweapon3.WeaponID = _weaponID;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon3Status(int _weaponStatus)
    //{
    //    tempweapon3.WeaponStatus = _weaponStatus;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon3Cost(int _weaponCost)
    //{
    //    tempweapon3.WeaponCost = _weaponCost;
    //    SavePlayerPrefsData();
    //}
    //public void SetTempWeapon3Damage(int _weaponDamage)
    //{
    //    tempweapon3.WeaponDamage = _weaponDamage;
    //    SavePlayerPrefsData();
    //}


    #endregion
    public void AddCoins(int amountToBeAdded)
    {
        Player.Coins += amountToBeAdded;
        PlayerPrefs.SetInt("playerCoins", Player.Coins);

        PlayerPrefs.Save();
    }
    public void AddInGameCoins(int amountToBeAdded)
    {
        inGameCoins += amountToBeAdded;
    }

    void RemoveCoins(int amountToBeRemoved)
    {
        Player.Coins -= amountToBeRemoved;
        PlayerPrefs.SetInt("playerCoins", Player.Coins);
        PlayerPrefs.Save();
    }
    public void AddInGameScore(int amountToBeAdded)
    {
        inGameScore += amountToBeAdded;
    }
}
