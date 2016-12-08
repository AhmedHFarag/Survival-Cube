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
    struct WeaponSlotData
    {
        public int ID;
        public bool Unlocked;
    }

    PlayerData Player;
    WaveData Wave;

    WeaponSlotData[] m_MainMenu_MainWeaponSlots=new WeaponSlotData[3];
    WeaponSlotData[] m_MainMenu_TempWeaponSlots = new WeaponSlotData[5];

    string MaxWaveReachedRef = "MaxWaveReached";
    string HighestWaveStreakRef = "HighestWaveStreak";

    MainWeaponData m_InGameMainWeapon;
    TempWeaponsData[] m_InGameTempweapons=new TempWeaponsData[3];

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
            m_InGameMainWeapon.ID = PlayerPrefs.GetInt("MainWeapon.ID");

        }
        else
        {
            PlayerPrefs.SetInt("MainWeapon.ID",0);
            m_InGameMainWeapon.ID = 0;
        }

        //Main menu Main Weapons Slots 
        for (int i = 0; i < m_MainMenu_MainWeaponSlots.Length; i++)
        {
            if (PlayerPrefs.HasKey("MainWeaponSlots"+i+".ID"))
            {
                m_MainMenu_MainWeaponSlots[i].ID = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".ID", i);
                m_MainMenu_MainWeaponSlots[i].ID = i;
            }

            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".Unlocked"))
            {
                if (PlayerPrefs.GetInt("MainWeaponSlots" + i + ".Unlocked")==0)
                {
                    m_MainMenu_MainWeaponSlots[i].Unlocked =false ;
                }
                else
                {
                    m_MainMenu_MainWeaponSlots[i].Unlocked = true;
                }
                
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".Unlocked", 0);
                m_MainMenu_MainWeaponSlots[i].Unlocked = false;
            }
        }
        //Satrt With First Single Shot 
        UnlockMainWeapon(0);


        //Main menu Temp Weapons Slots 
        for (int i = 0; i < m_MainMenu_TempWeaponSlots.Length; i++)
        {
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".ID"))
            {
                m_MainMenu_TempWeaponSlots[i].ID = PlayerPrefs.GetInt("TempWeaponSlots" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".ID", i);
                m_MainMenu_TempWeaponSlots[i].ID = i;
            }

            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".Unlocked"))
            {
                if (PlayerPrefs.GetInt("TempWeaponSlots" + i + ".Unlocked") == 0)
                {
                    m_MainMenu_TempWeaponSlots[i].Unlocked = false;
                }
                else
                {
                    m_MainMenu_TempWeaponSlots[i].Unlocked = true;
                }

            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".Unlocked", 0);
                m_MainMenu_TempWeaponSlots[i].Unlocked = false;
            }
        }

        for (int i = 0; i < m_InGameTempweapons.Length; i++)
        {
            m_InGameTempweapons[i].WeaponID = -1;
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
        PlayerPrefs.SetInt("MainWeapon.ID", m_InGameMainWeapon.ID);
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
        return m_InGameMainWeapon.ID;
    }
    public int GetTempWeapon(int index)
    {
        return m_InGameTempweapons[index].WeaponID;
    }
    public bool GetMainWeaponSlotStatus(int index)
    {
        return m_MainMenu_MainWeaponSlots[index].Unlocked;
    }
    public bool GetTempWeaponSlotStatus(int index)
    {
        return m_MainMenu_TempWeaponSlots[index].Unlocked;
    }
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
        m_InGameMainWeapon.ID = _weaponID;
        SavePlayerPrefsData();
    }

    public void SetTempWeapon(int index,int _weaponID)
    {
        m_InGameTempweapons[index].WeaponID = _weaponID;
    }

    public bool UnlockMainWeapon(int index)
    {
        if (m_MainMenu_MainWeaponSlots[index].Unlocked == false)
        {
            PlayerPrefs.SetInt("MainWeaponSlots" + index + ".Unlocked", 1);
            m_MainMenu_MainWeaponSlots[index].Unlocked = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool UnlockTempWeapon(int index)
    {
        if (m_MainMenu_TempWeaponSlots[index].Unlocked == false)
        {
            PlayerPrefs.SetInt("TempWeaponSlots" + index + ".Unlocked", 1);
            m_MainMenu_TempWeaponSlots[index].Unlocked = true;
            return true;
        }
        else
        {
            return false;
        }
    }
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
