using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public delegate void DataChanged();
public class DataHandler : MonoBehaviour
{
    public static event DataChanged DataChanged;
    struct TempWeaponsData
    {
      public int WeaponID;
      public bool UnlockStatus;
      public int WeaponCost;
      public int WeaponDamage;
    }
    
    struct BoostData
    {
        public int ID;
        public int Count;
        public int Is_Active;
    }
    struct EnergyBoostData
    {
        public int ID;
        public int Count;
        public int Cost;
        public int IsActive;
        public int Allowed;
    }
    struct FireRateBoostData
    {
        public int ID;
        public int Cost;
        public int Count;
        public int IsActive;
        public int Allowed;
    }
    struct DamageBoostData
    {
        public int ID;
        public int Count;
        public int Cost;
        public int IsActive;
        public int Allowed;
    }
    struct ShieldBoostData
    {
        public int ID;
        public int Count;
        public int Cost;
        public int IsActive;
        public int Allowed;
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
        public int Level;
        public int Is_Unlocked;

    }
    struct LevelData
    {
        public int LevelNumber;
        public int Is_Unlocked;

    }
    struct PlayerData
    {
        public string Name;
        public int Coins;
        public int HighScore;
        public int MaxWaveReached;
        public int HighestWaveStreak;
        public int MaxEnergy;
        public int ConsecutiveDays;
        public string LastPlayTime;
        public int StartingEnergy;
        public float EnergyRate;
        public float FireRateMultiplyer;
        public float DamageMultiplyer;
        public int Shield;
        
    }
    struct WeaponSlotData
    {
        public int ID;
        public bool Unlocked;
        public int FireRate;
        public int Damage;
    }

    PlayerData Player;
    WaveData Wave;

    WeaponSlotData[] m_MainMenu_MainWeaponSlots=new WeaponSlotData[3];
    WeaponSlotData[] m_MainMenu_TempWeaponSlots = new WeaponSlotData[4];
    BoostData[] Boosts = new BoostData[12];
    EnergyBoostData[] EnergyBoosts = new EnergyBoostData[4];
    FireRateBoostData[] FireRateBoosts = new FireRateBoostData[3];
    DamageBoostData[] DamageBoosts = new DamageBoostData[1];
    ShieldBoostData[] ShieldBoosts = new ShieldBoostData[4];
    LevelData[] Levels = new LevelData[10];
    WaveData[] Waves = new WaveData[200];
    string MaxWaveReachedRef = "MaxWaveReached";
    string HighestWaveStreakRef = "HighestWaveStreak";

    MainWeaponData m_InGameMainWeapon;
    public TempWeaponsData[] m_InGameTempweapons=new TempWeaponsData[3];

    public static DataHandler Instance;
    [HideInInspector]
    string PlayerName;
    [HideInInspector]
    float BG_Volume;
    [HideInInspector]
    int SFX_Volume;
    [HideInInspector]
    int inGameCoins;
    [HideInInspector]
    int inGameScore;

    [HideInInspector]
    int Energy;
    public bool DataLoaded = false;

    private static void OnDataChange()
    {
        var handler = DataChanged;
        if (handler != null) handler();
    }

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
        initializeData();
        //Start With First Single Shot 
        UnlockMainWeapon(0);
        //AddCoins(100000);
        //OnDataChange();
        DataLoaded = true;
    }

    // Use this for initialization
    void Start()
    {
        
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
        if(PlayerPrefs.HasKey("ConsecutiveDays"))
        {
            Player.ConsecutiveDays = PlayerPrefs.GetInt("ConsecutiveDays");

        }
        else
        {
            PlayerPrefs.SetInt("ConsecutiveDays", 0);
            Player.ConsecutiveDays = 0;
        }
        if(PlayerPrefs.HasKey("LastPlayTime"))
        {
            TimeSpan ts = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("LastPlayTime"));
            if(ts.TotalHours>24 && ts.TotalHours<48)
            {
                Player.ConsecutiveDays += 1;
                PlayerPrefs.SetString("LastPlayTime",Convert.ToString(DateTime.Now));
            }
            else if(ts.TotalHours>48)
            {
                Player.ConsecutiveDays = 0;
                PlayerPrefs.SetString("LastPlayTime", Convert.ToString(DateTime.Now));
            }
            Player.LastPlayTime = PlayerPrefs.GetString("LastPlayTime");
        }
        else
        {
            Player.LastPlayTime = PlayerPrefs.GetString("LastPlayTime");

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
        if(PlayerPrefs.HasKey("MaxEnergy"))
        {
            Player.MaxEnergy = PlayerPrefs.GetInt("MaxEnergy");
        }
        else
        {
            PlayerPrefs.SetInt("MaxEnergy", 100);
            Player.MaxEnergy = 100;
        }
        if(PlayerPrefs.HasKey("StartingEnergy"))
        {
            Player.StartingEnergy = PlayerPrefs.GetInt("StartingEnergy");
        }
        else
        {
            PlayerPrefs.SetInt("StartingEnergy", 0);
            Player.StartingEnergy= PlayerPrefs.GetInt("StartingEnergy");
        }
        if(PlayerPrefs.HasKey("EnergyRate"))
        {
            Player.EnergyRate = PlayerPrefs.GetFloat("EnergyRate");
        }
        else
        {
            PlayerPrefs.SetFloat("EnergyRate", 5);
            Player.EnergyRate = 5;
        }
        if(PlayerPrefs.HasKey("FireRateMultiplyer"))
        {
            Player.FireRateMultiplyer = PlayerPrefs.GetFloat("FireRateMultiplyer");
        }
        else
        {
            PlayerPrefs.SetFloat("FireRateMultiplyer", 1);
            Player.FireRateMultiplyer = 1;
        }
        if (PlayerPrefs.HasKey("DamageMultiplyer"))
        {
            Player.DamageMultiplyer = PlayerPrefs.GetFloat("DamageMultiplyer");
        }
        else
        {
            PlayerPrefs.SetFloat("DamageMultiplyer", 1);
            Player.DamageMultiplyer = 1;
        }
        if(PlayerPrefs.HasKey("Shield"))
        {
            Player.Shield = PlayerPrefs.GetInt("Shield");
        }
        else
        {
            PlayerPrefs.SetInt("Shield", 0);
            Player.Shield = 0;
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
        #region Main Weapons Initialization
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
            //Unlocked
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

            //Fire Rate
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".FireRate"))
            {
                m_MainMenu_MainWeaponSlots[i].FireRate = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".FireRate");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".FireRate", 1);
                m_MainMenu_MainWeaponSlots[i].FireRate = 1;
            }

            //Damage
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".Damage"))
            {
                m_MainMenu_MainWeaponSlots[i].Damage = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".Damage");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".Damage", 10);
                m_MainMenu_MainWeaponSlots[i].Damage = 10;
            }

        }
        #endregion

        #region Boost Initialization
        for (int i=0; i<Boosts.Length;i++)
        {
            if (PlayerPrefs.HasKey("Boost" + i + ".ID")) 
            {
                Boosts[i].ID = PlayerPrefs.GetInt("Boost" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("Boost" + i + ".ID", i);
                Boosts[i].ID = i;
            }
            if(PlayerPrefs.HasKey("Boost"+i+".Count"))
            {
                Boosts[i].Count = PlayerPrefs.GetInt("Boost" + i + ".Count");
            }
            else
            {
                PlayerPrefs.SetInt("Boost" + i + ".Count", 0);
                Boosts[i].Count = 0;
            }
            if (PlayerPrefs.HasKey("Boost" + i + ".Is_Active")) 
            {
                Boosts[i].Is_Active = PlayerPrefs.GetInt("Boost" + i + ".Is_Active");
            }
            else
            {
                PlayerPrefs.SetInt("Boost" + i + ".Is_Active", 0);
                Boosts[i].Is_Active = 0;
            }
        }
        #endregion
        #region Energy Boosts
        for (int i = 0; i < EnergyBoosts.Length; i++)
        {
            if (PlayerPrefs.HasKey("EnergyBoost" + i + ".ID"))
            {
                EnergyBoosts[i].ID = PlayerPrefs.GetInt("EnergyBoost" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("EnergyBoost" + i + ".ID", i);
                EnergyBoosts[i].ID = i;
            }
            if (PlayerPrefs.HasKey("EnergyBoost" + i + ".Count"))
            {
                EnergyBoosts[i].Count = PlayerPrefs.GetInt("EnergyBoost" + i + ".Count");
            }
            else
            {
                PlayerPrefs.SetInt("EnergyBoost" + i + ".Count", 0);
                EnergyBoosts[i].Count = 0;
            }
            if (PlayerPrefs.HasKey("EnergyBoost" + i + ".Is_Active"))
            {
                EnergyBoosts[i].IsActive = PlayerPrefs.GetInt("EnergyBoost" + i + ".Is_Active");
            }
            else
            {
                PlayerPrefs.SetInt("EnergyBoost" + i + ".Is_Active", 0);
                EnergyBoosts[i].IsActive = 0;
            }
            if(PlayerPrefs.HasKey("EnergyBoost" + i + ".Allowed"))
            {
                EnergyBoosts[i].Allowed = PlayerPrefs.GetInt("EnergyBoost" + i + ".Allowed");
            }
            else
            {
                PlayerPrefs.SetInt("EnergyBoost" + i + ".Allowed", 1);
                EnergyBoosts[i].Allowed = 1;
            }
            if (PlayerPrefs.HasKey("EnergyBoost" + i + ".Cost"))
            {
                EnergyBoosts[i].Cost = PlayerPrefs.GetInt("EnergyBoost" + i + ".Cost");
            }
            else
            {
                PlayerPrefs.SetInt("EnergyBoost" + i + ".Cost", 0);
                EnergyBoosts[i].Cost = 0;
            }
        }
        #endregion
        #region FireRate Boosts
        for (int i = 0; i < FireRateBoosts.Length; i++)
        {
            if (PlayerPrefs.HasKey("FireRateBoost" + i + ".ID"))
            {
                FireRateBoosts[i].ID = PlayerPrefs.GetInt("FireRateBoost" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("FireRateBoost" + i + ".ID", i);
                FireRateBoosts[i].ID = i;
            }
            if (PlayerPrefs.HasKey("FireRateBoost" + i + ".Count"))
            {
                FireRateBoosts[i].Count = PlayerPrefs.GetInt("FireRateBoost" + i + ".Count");
            }
            else
            {
                PlayerPrefs.SetInt("FireRateBoost" + i + ".Count", 0);
                FireRateBoosts[i].Count = 0;
            }
            if (PlayerPrefs.HasKey("FireRateBoost" + i + ".Is_Active"))
            {
                FireRateBoosts[i].IsActive = PlayerPrefs.GetInt("FireRateBoost" + i + ".Is_Active");
            }
            else
            {
                PlayerPrefs.SetInt("FireRateBoost" + i + ".Is_Active", 0);
                FireRateBoosts[i].IsActive = 0;
            }
            if (PlayerPrefs.HasKey("FireRateBoost" + i + ".Allowed"))
            {
                FireRateBoosts[i].Allowed = PlayerPrefs.GetInt("FireRateBoost" + i + ".Allowed");
            }
            else
            {
                PlayerPrefs.SetInt("FireRateBoost" + i + ".Allowed", 1);
                FireRateBoosts[i].Allowed = 1;
            }
            if (PlayerPrefs.HasKey("FireRateBoost" + i + ".Cost"))
            {
                FireRateBoosts[i].Cost = PlayerPrefs.GetInt("FireRateBoost" + i + ".Cost");
            }
            else
            {
                PlayerPrefs.SetInt("FireRateBoost" + i + ".Cost", 0);
                FireRateBoosts[i].Cost = 0;
            }
        }
        #endregion
        #region Damage Boosts

        for (int i = 0; i < DamageBoosts.Length; i++)
        {
            if (PlayerPrefs.HasKey("DamageBoost" + i + ".ID"))
            {
                DamageBoosts[i].ID = PlayerPrefs.GetInt("DamageBoost" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("DamageBoost" + i + ".ID", i);
                DamageBoosts[i].ID = i;
            }
            if (PlayerPrefs.HasKey("DamageBoost" + i + ".Count"))
            {
                DamageBoosts[i].Count = PlayerPrefs.GetInt("DamageBoost" + i + ".Count");
            }
            else
            {
                PlayerPrefs.SetInt("DamageBoost" + i + ".Count", 0);
                DamageBoosts[i].Count = 0;
            }
            if (PlayerPrefs.HasKey("DamageBoost" + i + ".Is_Active"))
            {
                DamageBoosts[i].IsActive = PlayerPrefs.GetInt("DamageBoost" + i + ".Is_Active");
            }
            else
            {
                PlayerPrefs.SetInt("DamageBoost" + i + ".Is_Active", 0);
                DamageBoosts[i].IsActive = 0;
            }
            if (PlayerPrefs.HasKey("DamageBoost" + i + ".Allowed"))
            {
                DamageBoosts[i].Allowed = PlayerPrefs.GetInt("DamageBoost" + i + ".Allowed");
            }
            else
            {
                PlayerPrefs.SetInt("DamageBoost" + i + ".Allowed", 1);
                DamageBoosts[i].Allowed = 1;
            }
            if (PlayerPrefs.HasKey("DamageBoost" + i + ".Cost"))
            {
                DamageBoosts[i].Allowed = PlayerPrefs.GetInt("DamageBoost" + i + ".Cost");
            }
            else
            {
                PlayerPrefs.SetInt("DamageBoost" + i + ".Cost", 0);
                DamageBoosts[i].Cost = 0;
            }
        }
        #endregion
        #region Shield Boosts
        for (int i = 0; i < ShieldBoosts.Length; i++)
        {
            if (PlayerPrefs.HasKey("ShieldBoost" + i + ".ID"))
            {
                ShieldBoosts[i].ID = PlayerPrefs.GetInt("ShieldBoost" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("ShieldBoost" + i + ".ID", i);
                ShieldBoosts[i].ID = i;
            }
            if (PlayerPrefs.HasKey("ShieldBoost" + i + ".Count"))
            {
                ShieldBoosts[i].Count = PlayerPrefs.GetInt("ShieldBoost" + i + ".Count");
            }
            else
            {
                PlayerPrefs.SetInt("ShieldBoost" + i + ".Count", 0);
                ShieldBoosts[i].Count = 0;
            }
            if (PlayerPrefs.HasKey("ShieldBoost" + i + ".Is_Active"))
            {
                ShieldBoosts[i].IsActive = PlayerPrefs.GetInt("ShieldBoost" + i + ".Is_Active");
            }
            else
            {
                PlayerPrefs.SetInt("ShieldBoost" + i + ".Is_Active", 0);
                ShieldBoosts[i].IsActive = 0;
            }
            if (PlayerPrefs.HasKey("ShieldBoost" + i + ".Allowed"))
            {
                ShieldBoosts[i].Allowed = PlayerPrefs.GetInt("ShieldBoost" + i + ".Allowed");
            }
            else
            {
                PlayerPrefs.SetInt("ShieldBoost" + i + ".Allowed", 1);
                ShieldBoosts[i].Allowed = 1;
            }
            if (PlayerPrefs.HasKey("ShieldBoost" + i + ".Cost"))
            {
                ShieldBoosts[i].Cost = PlayerPrefs.GetInt("ShieldBoost" + i + ".Cost");
            }
            else
            {
                PlayerPrefs.SetInt("ShieldBoost" + i + ".Cost", 0);
                ShieldBoosts[i].Cost = 0;
            }
        }
        #endregion

        #region Temp Weapons Initialization
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
            m_InGameTempweapons[i].UnlockStatus = false;
        }
        m_InGameTempweapons[0].UnlockStatus = true;
        #endregion

        //Master Volume
        if (PlayerPrefs.HasKey("bgVolume"))
        {
            BG_Volume = PlayerPrefs.GetFloat("bgVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("bgVolume", 1);
            BG_Volume = 1;
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
        #region Level Data
        for (int i = 0; i < Levels.Length; i++)
        {
            if (PlayerPrefs.HasKey("Level" + i + "Number"))
            {
                Levels[i].LevelNumber = PlayerPrefs.GetInt("LevelNumber" + i);
            }
            else
            {
                Levels[i].LevelNumber = i + 1;
                PlayerPrefs.SetInt("LevelNumber" + i, i + 1);
            }
            if (PlayerPrefs.HasKey("Level" + i + ".IsUnlocked"))
            {
                Levels[i].Is_Unlocked = PlayerPrefs.GetInt("Level" + i + ".IsUnlocked");
            }
            else
            {
                Levels[i].Is_Unlocked = (i == 0 ? 1 : 0);
                PlayerPrefs.SetInt("Level" + i + ".IsUnlocked", Levels[i].Is_Unlocked);
            }
            for (int j = 0; j < 20; j++)
            {
                int z = i * 10 + j;
                if (PlayerPrefs.HasKey("Wave" + z + ".Number"))
                {
                    Waves[j].WaveNumber = PlayerPrefs.GetInt("Wave" + z + ".Number");
                }
                else
                {
                    Waves[j].WaveNumber = z + 1;
                    PlayerPrefs.SetInt("Wave" + z + ".Number", z + 1);
                }
                if (PlayerPrefs.HasKey("Wave" + z + ".IsUnlocked"))
                {
                    Waves[j].Is_Unlocked = PlayerPrefs.GetInt("Wave" + z + ".IsUnlocked");
                }
                else
                {
                    Waves[j].Is_Unlocked = (z == 0 ? 1 : 0);
                    PlayerPrefs.SetInt("Wave" + z + ".IsUnlocked", Waves[j].Is_Unlocked);
                }
            }
        }
        #endregion
        
        //Saving 
        PlayerPrefs.Save();
    }
    public void ResetPlayerInGameData()
    {
        inGameCoins = 0;
        inGameScore = 0;
        Energy = 0;
        //Player.MaxWaveReached = 0;
        //Player.HighestWaveStreak = 0;
        //playerCoins = 0;
        //PlayerName = "";
        //PlayerPrefs.DeleteAll();
        //initializeData();

    }
    public void ResetAllPlayerSavedData()
    {
        PlayerPrefs.DeleteAll();
        initializeData();
        OnDataChange();
    }
    public void SavePlayerPrefsData()
    {
        PlayerPrefs.SetInt("playerCoins", Player.Coins);
        PlayerPrefs.SetInt("inGameCoins", inGameCoins);
        PlayerPrefs.SetString("playerName", PlayerName);
        PlayerPrefs.SetFloat("bgVolume", BG_Volume);
        PlayerPrefs.SetInt("sfxVolume", SFX_Volume);
        PlayerPrefs.SetInt("WaveNo", Wave.WaveNumber);
        PlayerPrefs.SetInt("MaxEnergy", Player.MaxEnergy);
        //PlayerPrefs.SetInt("InGameScore", inGameScore);
        PlayerPrefs.SetInt("BestScore", Player.HighScore);
        PlayerPrefs.SetInt("MainWeapon.ID", m_InGameMainWeapon.ID);
        PlayerPrefs.Save();
        OnDataChange();
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
    public int GetInGameEnergy()
    {
        return Energy;
    }
    public float GetBgVolume()
    {
      
        return BG_Volume;
       
    }
    public int GetStartingEnergy()
    {
        return Player.StartingEnergy;
    }
    public float GetPlayerEnergyRate()
    {
        return Player.EnergyRate;
    }
    public string GetPlayerName()
    {
        return Player.Name;
    }

    #region Wave & level getters
    public int GetWaveNumber(int index)
    {
        return Waves[index].WaveNumber;
    }
    public int GetWaveUnlocked(int index)
    {
        return Waves[index].Is_Unlocked;
    }
    public int GetMaxWaveReached()
    {
        return Player.MaxWaveReached;
    }
    public int GetHighestWaveStreak()
    {
        return Player.HighestWaveStreak;
    }
    public int GetLevelNumber(int index)
    {
        return Levels[index].LevelNumber;
    }
    public int GetLevelUnlocked(int index)
    {
        return Levels[index].Is_Unlocked;
    }
    #endregion
    public int GetConsecutiveDays()
    {
        return Player.ConsecutiveDays;
    }
    public string GetLastTimePlayed()
    {
        return Player.LastPlayTime;
    }
    public float GetFireRateMultiplyer()
    {
        return Player.FireRateMultiplyer;
    }
    public float GetDamageMltiplyer()
    {
        return Player.DamageMultiplyer;
    }
    public int GetShield()
    {
        return Player.Shield;
    }
    public int GetBestScore()
    {
        return Player.HighScore;
    }

    public string GetBestScoreStr()
    {
        return Player.HighScore.ToString();
    }
    public int GetMaxEnergy()
    {
        return Player.MaxEnergy;
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
    public int GetBoost(int index)
    {
        return Boosts[index].ID;
    }
    public int GetBoostCount(int index)
    {
        return Boosts[index].Count;
    }
    public int GetBoostStatus(int index)
    {
        return Boosts[index].Is_Active;
    }
    public bool GetMainWeaponSlotStatus(int index)
    {
        return m_MainMenu_MainWeaponSlots[index].Unlocked;
    }
    public bool GetTempWeaponSlotStatus(int index)
    {
        return m_MainMenu_TempWeaponSlots[index].Unlocked;
    }
    public List<int> GetUnlockedMainWeaponsIDs()
    {
        List<int> IDs = new List<int>();
        foreach (var item in m_MainMenu_MainWeaponSlots)
        {
            if (item.Unlocked)
            {
                IDs.Add(item.ID);
            }
        }
        return IDs;

    }
    public List<int> GetUnlockedTempWeaponsIDs()
    {
        List<int> IDs = new List<int>();
        foreach (var item in m_MainMenu_TempWeaponSlots)
        {
            if (item.Unlocked)
            {
                IDs.Add(item.ID);
            }
        }
        return IDs;

    }
    public List<int> GetLockedMainWeaponsIDs()
    {
        List<int> IDs = new List<int>();
        foreach (var item in m_MainMenu_MainWeaponSlots)
        {
            if (item.Unlocked==false)
            {
                IDs.Add(item.ID);
            }
        }
        return IDs;

    }
    public List<int> GetLockedTempWeaponsIDs()
    {
        List<int> IDs = new List<int>();
        foreach (var item in m_MainMenu_TempWeaponSlots)
        {
            if (item.Unlocked == false)
            {
                IDs.Add(item.ID);
            }
        }
        return IDs;

    }
    #region Boost Getters
    public int GetEnergyBoostID(int index)
    {
        return EnergyBoosts[index].ID;
    }
    public int GetEnergyBoostCount(int index)
    {
        return EnergyBoosts[index].Count;
    }
    public int GetEnergyBoostActive(int index)
    {
        return EnergyBoosts[index].IsActive;
    }
    public int GetEnergyBoostAllowed(int index)
    {
        return EnergyBoosts[index].Allowed;
    }
    public int GetEnergyBoostCost(int index)
    {
        return EnergyBoosts[index].Cost;
    }
    public int GetFireRateBoostID(int index)
    {
        return FireRateBoosts[index].ID;
    }
    public int GetFireRateBoostCount(int index)
    {
        return FireRateBoosts[index].Count;
    }
    public int GetFireRateBoostActive(int index)
    {
        return FireRateBoosts[index].IsActive;
    }
    public int GetFireRateBoostAllowed(int index)
    {
        return FireRateBoosts[index].Allowed;
    }
    public int GetFireRateBoostCost(int index)
    {
        return FireRateBoosts[index].Cost;
    }
    public int GetDamageBoostID(int index)
    {
        return DamageBoosts[index].ID;
    }
    public int GetDamageBoostCount(int index)
    {
        return DamageBoosts[index].Count;
    }
    public int GetDamageBoostActive(int index)
    {
        return DamageBoosts[index].IsActive;
    }
    public int GetDamageBoostAllowed(int index)
    {
        return DamageBoosts[index].Allowed;
    }
    public int GetDamageBoostCost(int index)
    {
        return DamageBoosts[index].Cost;
    }
    public int GetShieldBoostID(int index)
    {
        return ShieldBoosts[index].ID;
    }
    public int GetShieldBoostCount(int index)
    {
        return ShieldBoosts[index].Count;
    }
    public int GetShieldBoostActive(int index)
    {
        return ShieldBoosts[index].IsActive;
    }
    public int GetShieldBoostAllowed(int index)
    {
        return ShieldBoosts[index].Allowed;
    }
    public int GetShieldBoostCost(int index)
    {
        return ShieldBoosts[index].Cost;
    }
    #endregion
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
    public void SetBgVolume(float _bgVolume)
    {
        BG_Volume = _bgVolume;
        PlayerPrefs.SetFloat("bgVolume", _bgVolume);

        PlayerPrefs.Save();
    }
    public void SetPlayerMaxEnergy(int energy)
    {
        Player.MaxEnergy = energy;
        PlayerPrefs.SetInt("MaxEnergy", energy);
        PlayerPrefs.Save();
    }
    public void SetPlayerStartingEnergy(int energy)
    {
        Player.StartingEnergy = energy;
        PlayerPrefs.SetInt("StartingEnergy", energy);
        PlayerPrefs.Save();
    }
    public void SetPlayerEnergyRate(float rate)
    {
        Player.EnergyRate = rate;
        PlayerPrefs.SetFloat("EnergyRate", rate);
        PlayerPrefs.Save();
    }
    #region Wave & Level Setters
    public void SetWaveNumber(int index, int _WaveNo)
    {
        Waves[index].WaveNumber = _WaveNo;
        PlayerPrefs.SetInt("Wave" + index + ".Number", _WaveNo);
        PlayerPrefs.Save();

    }
    public void SetWaveUnlocked(int index)
    {
        Waves[index].Is_Unlocked = 1;
        PlayerPrefs.SetInt("Wave" + index + ".IsUnlocked", 1);
        PlayerPrefs.Save();
    }
    public void SetLevelNumber(int index, int _LvlNo)
    {
        Levels[index].LevelNumber = _LvlNo;
        PlayerPrefs.SetInt("Level" + index + ".Number", _LvlNo);
        PlayerPrefs.Save();

    }
    public void SetLevelUnlocked(int index)
    {
        Levels[index].Is_Unlocked = 1;
        PlayerPrefs.SetInt("Level" + index + ".IsUnlocked", 1);
        PlayerPrefs.Save();
    }
    #endregion
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
    public void SetFireRateMultiplyer(float rate)
    {
        Player.FireRateMultiplyer = rate;
        PlayerPrefs.SetFloat("FireRateMultiplyer", rate);
        PlayerPrefs.Save();
    }
    public void SetDamageMultiplyer(float rate)
    {
        Player.DamageMultiplyer = rate;
        PlayerPrefs.SetFloat("DamageMultiplyer", rate);
        PlayerPrefs.Save();
    }
    public void SetShield(int shield)
    {
        Player.Shield = shield;
        PlayerPrefs.SetInt("Shield", shield);
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

    public void SetMainWeaponID(int _weaponID)
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
            OnDataChange();
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
            OnDataChange();
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Boost Setters
    public void AddEnergyBoost(int index, int count)
    {
        EnergyBoosts[index].Count += count;
        PlayerPrefs.SetInt("EnergyBoost" + index + ".Count", EnergyBoosts[index].Count);
        PlayerPrefs.Save();
    }
    public void ToggleEnergyBoost(int index, int isActive)
    {
        EnergyBoosts[index].IsActive = isActive;
        PlayerPrefs.SetInt("EnergyBoost" + index + ".Is_Active", isActive);
        for(int i=0; i<EnergyBoosts.Length;i++)
        {
            EnergyBoosts[i].Allowed = Mathf.Abs(isActive - 1);
            PlayerPrefs.SetInt("EnergyBoost" + i + ".Allowed", Mathf.Abs(isActive - 1));
        }
    }
    public void SetEnergyBoostCost(int index, int cost)
    {
        EnergyBoosts[index].Cost = cost;
        PlayerPrefs.SetInt("EnergyBoost" + index + ".Cost", cost);
        PlayerPrefs.Save();
    }
    public void AddFireRateBoost(int index, int count)
    {
        FireRateBoosts[index].Count += count;
        PlayerPrefs.SetInt("FireRateBoost" + index + ".Count", FireRateBoosts[index].Count);
        PlayerPrefs.Save();
    }
    public void ToggleFireRateBoost(int index, int isActive)
    {
        FireRateBoosts[index].IsActive = isActive;
        PlayerPrefs.SetInt("FireRateBoost" + index + ".Is_Active", isActive);
        for (int i = 0; i < FireRateBoosts.Length; i++)
        {
            FireRateBoosts[i].Allowed = Mathf.Abs(isActive - 1);
            PlayerPrefs.SetInt("FireRateBoost" + i + ".Allowed", Mathf.Abs(isActive - 1));
        }
    }
    public void SetFireRateBoostCost(int index, int cost)
    {
        FireRateBoosts[index].Cost = cost;
        PlayerPrefs.SetInt("FireRateBoost" + index + ".Cost", cost);
        PlayerPrefs.Save();
    }
    public void AddDamageBoost(int index, int count)
    {
        DamageBoosts[index].Count += count;
        PlayerPrefs.SetInt("DamageBoost" + index + ".Count", DamageBoosts[index].Count);
        PlayerPrefs.Save();
    }
    public void ToggleDamageBoost(int index, int isActive)
    {
        DamageBoosts[index].IsActive = isActive;
        PlayerPrefs.SetInt("DamageBoost" + index + ".Is_Active", isActive);
        for (int i = 0; i < DamageBoosts.Length; i++)
        {
            DamageBoosts[i].Allowed = Mathf.Abs(isActive - 1);
            PlayerPrefs.SetInt("DamageBoost" + i + ".Allowed", Mathf.Abs(isActive - 1));
        }
    }
    public void SetDamageBoostCost(int index, int cost)
    {
        DamageBoosts[index].Cost = cost;
        PlayerPrefs.SetInt("DamageBoost" + index + ".Cost", cost);
        PlayerPrefs.Save();
    }
    public void AddShieldBoost(int index, int count)
    {
        ShieldBoosts[index].Count += count;
        PlayerPrefs.SetInt("ShieldBoost" + index + ".Count", ShieldBoosts[index].Count);
        PlayerPrefs.Save();
    }
    public void ToggleShieldBoost(int index, int isActive)
    {
        ShieldBoosts[index].IsActive = isActive;
        PlayerPrefs.SetInt("ShieldBoost" + index + ".Is_Active", isActive);
        for (int i = 0; i < ShieldBoosts.Length; i++)
        {
            ShieldBoosts[i].Allowed = Mathf.Abs(isActive - 1);
            PlayerPrefs.SetInt("ShieldBoost" + i + ".Allowed", Mathf.Abs(isActive - 1));
        }
    }
    public void SetShieldBoostCost(int index, int cost)
    {
        ShieldBoosts[index].Cost = cost;
        PlayerPrefs.SetInt("ShieldBoost" + index + ".Cost", cost);
        PlayerPrefs.Save();
    }
    #endregion
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
    public void AddEnergy(int energy)
    {
        Energy += energy;
        if(Energy>Player.MaxEnergy)
        {
            Energy = Player.MaxEnergy;
        }
    }
    public void SubtractEnergy(int energy)
    {
        Energy -= energy;
        if(Energy<0)
        {
            Energy = 0;
        }
    }
    void RemoveCoins(int amountToBeRemoved)
    {
        Player.Coins -= amountToBeRemoved;
        PlayerPrefs.SetInt("playerCoins", Player.Coins);
        PlayerPrefs.Save();
    }
    public void AddBoost(int index)
    {
        Boosts[index].Count+=1;
        PlayerPrefs.SetInt("Boost" + index + ".Count", Boosts[index].Count);
        PlayerPrefs.Save();
    }
    public void ActivateBoost(int index)
    {
        Boosts[index].Count -= 1;
        Boosts[index].Is_Active = 1;
        PlayerPrefs.SetInt("Boost" + index + ".Count", Boosts[index].Count);
        PlayerPrefs.SetInt("Boost" + index + ".Is_Active", 1);

        PlayerPrefs.Save();
    }
    public void AddInGameScore(int amountToBeAdded)
    {
        inGameScore += amountToBeAdded;
    }

    public void MainMenuWasLoaded()
    {
        OnDataChange();
    }
}
