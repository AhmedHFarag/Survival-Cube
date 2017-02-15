using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public delegate void DataChanged();
public class DataHandler : MonoBehaviour
{
    public static event DataChanged DataChanged;
    public struct TempWeaponsData
    {
      public int WeaponID;
      public bool UnlockStatus;
      public int WeaponCost;
      public int WeaponDamage;
      public int Level;
    }
    public struct SlowTimeData
    {
        public float Duration;
        public float SlowFactor;
        public int UpgradeCost;
    }
    public struct WaveClearData
    {
        public int Damage;
        public int UpgradeCost;
    }
    public struct ElectricData
    {
        public int Damage;
        public float Duration;
        public int UpgradeCost;
    }
    public struct TurretMultiplyData
    {
        public int ExtraTurrets;
        public int UpgradeCost;
    }
    public struct UpgradeMainWeaponData
    {
        public int UpgradeIndex;
        public int WeaponIndex;
        public int UpgradeCost;
        public float FireRate;
        public int Damage;
        public float Duration;
        public float SlowFactor;
        public int ExtraTurrets;
    }
    public struct UpgradeTempWeaponData
    {
        public int UpgradeIndex;
        public int WeaponIndex;
        public int UpgradeCost;
        public int Damage;
        public float FireRate;
        public float Duration;
        public float SlowFactor;
        public float ExtraTurrets;
    }
    public struct BoostData
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
       public float FireRate;
       public int Level;
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
        public float FireRate;
        public int Damage;
        public int Level;
        public float Duration;
        public float SlowFactor;
        public int ExtraTurrets;
    }
    struct BundlesData
    {
        public float CoinPrice100;
        public float CoinPrice500;
        public float CoinPrice1000;
        public float CoinPrice2000;
        public float CoinPrice4000;
        public float CoinPrice10000;
    }
    PlayerData Player;
    WaveClearData ClearWeapon;
    SlowTimeData SlowWeapon;
    ElectricData ElectricWeapon;
    TurretMultiplyData MultipleWeapon;
    WaveData Wave;

    WeaponSlotData[] MainWeapons= new WeaponSlotData[2];
    WeaponSlotData[] TempWeapons = new WeaponSlotData[4];
    UpgradeMainWeaponData[] MainWeaponUpgs = new UpgradeMainWeaponData[6];
    UpgradeTempWeaponData[] TempWeaponUpgs = new UpgradeTempWeaponData[15];
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
    TempWeaponsData[] m_InGameTempweapons=new TempWeaponsData[3];
    BundlesData Bundle;

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
        for (int i = 0; i < MainWeapons.Length; i++)
        {
            if (PlayerPrefs.HasKey("MainWeaponSlots"+i+".ID"))
            {
                MainWeapons[i].ID = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".ID", i);
                MainWeapons[i].ID = i;
            }
            //Unlocked
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".Unlocked"))
            {
                if (PlayerPrefs.GetInt("MainWeaponSlots" + i + ".Unlocked")==0)
                {
                    MainWeapons[i].Unlocked =false ;
                }
                else
                {
                    MainWeapons[i].Unlocked = true;
                }
                
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".Unlocked", 0);
                MainWeapons[i].Unlocked = false;
            }

            //Fire Rate
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".FireRate"))
            {
                MainWeapons[i].FireRate = PlayerPrefs.GetFloat("MainWeaponSlots" + i + ".FireRate");
            }
            else
            {
                PlayerPrefs.SetFloat("MainWeaponSlots" + i + ".FireRate", 1);
                MainWeapons[i].FireRate = 1;
            }

            //Damage
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".Damage"))
            {
                MainWeapons[i].Damage = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".Damage");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".Damage", 10);
                MainWeapons[i].Damage = 10;
            }
            if(PlayerPrefs.HasKey("MainWeaponSlots" + i + ".Level"))
            {
                MainWeapons[i].Level = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".Level");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".Level", 0);
                MainWeapons[i].Level = 0;
            }
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".Duration"))
            {
                MainWeapons[i].Duration = PlayerPrefs.GetFloat("MainWeaponSlots" + i + ".Duration");
            }
            else
            {
                PlayerPrefs.SetFloat("MainWeaponSlots" + i + ".Duration", 5);
                MainWeapons[i].Duration = 5;
            }
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".SlowFactor"))
            {
                MainWeapons[i].SlowFactor = PlayerPrefs.GetFloat("MainWeaponSlots" + i + ".SlowFactor");
            }
            else
            {
                PlayerPrefs.SetFloat("MainWeaponSlots" + i + ".SlowFactor", 1);
                MainWeapons[i].SlowFactor = 1;
            }
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".ExtraTurrets"))
            {
                MainWeapons[i].ExtraTurrets = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".ExtraTurrets");
            }
            else
            {
                PlayerPrefs.SetInt("MainWeaponSlots" + i + ".ExtraTurrets", 0);
                MainWeapons[i].ExtraTurrets = 0;
            }
            if (PlayerPrefs.HasKey("MainWeaponSlots" + i + ".FireRate"))
            {
                MainWeapons[i].FireRate = PlayerPrefs.GetFloat("MainWeaponSlots" + i + ".FireRate");
            }
            else
            {
                PlayerPrefs.SetFloat("MainWeaponSlots" + i + ".FireRate", 1);
                MainWeapons[i].FireRate = 1;
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
        for (int i = 0; i < TempWeapons.Length; i++)
        {
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".ID"))
            {
                TempWeapons[i].ID = PlayerPrefs.GetInt("TempWeaponSlots" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".ID", i);
                TempWeapons[i].ID = i;
            }

            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".Unlocked"))
            {
                if (PlayerPrefs.GetInt("TempWeaponSlots" + i + ".Unlocked") == 0)
                {
                    TempWeapons[i].Unlocked = false;
                }
                else
                {
                    TempWeapons[i].Unlocked = true;
                }

            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".Unlocked", 0);
                TempWeapons[i].Unlocked = false;
            }
            if(PlayerPrefs.HasKey("TempWeaponSlots" + i + ".Level"))
            {
                TempWeapons[i].Level = PlayerPrefs.GetInt("TempWeaponSlots" + i + ".Level");
            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".Level", 0);
                TempWeapons[i].Level = 0;
            }
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".Duration"))
            {
                TempWeapons[i].Duration = PlayerPrefs.GetFloat("TempWeaponSlots" + i + ".Duration");
            }
            else
            {
                PlayerPrefs.SetFloat("TempWeaponSlots" + i + ".Duration", 5);
                TempWeapons[i].Duration = 5;
            }
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".SlowFactor"))
            {
                TempWeapons[i].SlowFactor = PlayerPrefs.GetFloat("TempWeaponSlots" + i + ".SlowFactor");
            }
            else
            {
                PlayerPrefs.SetFloat("TempWeaponSlots" + i + ".SlowFactor", 1);
                TempWeapons[i].SlowFactor = 1;
            }
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".ExtraTurrets"))
            {
                TempWeapons[i].ExtraTurrets = PlayerPrefs.GetInt("TempWeaponSlots" + i + ".ExtraTurrets");
            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".ExtraTurrets", 0);
                TempWeapons[i].ExtraTurrets = 0;
            }
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".FireRate"))
            {
                TempWeapons[i].FireRate = PlayerPrefs.GetFloat("TempWeaponSlots" + i + ".FireRate");
            }
            else
            {
                PlayerPrefs.SetFloat("TempWeaponSlots" + i + ".FireRate", 1);
                TempWeapons[i].FireRate = 1;
            }
            if (PlayerPrefs.HasKey("TempWeaponSlots" + i + ".Damage"))
            {
                TempWeapons[i].Damage = PlayerPrefs.GetInt("TempWeaponSlots" + i + ".Damage");
            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponSlots" + i + ".Damage", 10);
                TempWeapons[i].Damage = 10;
            }
        }
        if(PlayerPrefs.HasKey("ClearWeapon.Damage"))
        {
            ClearWeapon.Damage = PlayerPrefs.GetInt("ClearWeapon.Damage");
        }
        else
        {
            ClearWeapon.Damage = 100;
            PlayerPrefs.SetInt("ClearWeapon.Damage", 100);
        }
        if(PlayerPrefs.HasKey("ClearWeapon.UpgradeCost"))
        {
            ClearWeapon.UpgradeCost = PlayerPrefs.GetInt("ClearWeapon.UpgradeCost");
        }
        else
        {
            ClearWeapon.UpgradeCost = 200;
            PlayerPrefs.SetInt("ClearWeapon.UpgradeCost", 200);
        }

        #endregion

        #region Temp Weapons InGameSlots

        for (int i = 0; i < m_InGameTempweapons.Length; i++)
        {
            if (PlayerPrefs.HasKey("TempWeaponInGameSlots" + i + ".ID"))
            {
                m_InGameTempweapons[i].WeaponID = PlayerPrefs.GetInt("TempWeaponInGameSlots" + i + ".ID");
            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponInGameSlots" + i + ".ID", -1);
                m_InGameTempweapons[i].WeaponID = -1;
            }
            if (PlayerPrefs.HasKey("TempWeaponInGameSlots" + i + ".Unlocked"))
            {
                if (PlayerPrefs.GetInt("TempWeaponInGameSlots" + i + ".Unlocked") == 0)
                {
                    m_InGameTempweapons[i].UnlockStatus = false;
                }
                else
                {
                    m_InGameTempweapons[i].UnlockStatus = true;
                }

            }
            else
            {
                PlayerPrefs.SetInt("TempWeaponInGameSlots" + i + ".Unlocked", 0);
                m_InGameTempweapons[i].UnlockStatus = false;
            }

            //m_InGameTempweapons[i].WeaponID = -1;
            //m_InGameTempweapons[i].UnlockStatus = false;
        }
        m_InGameTempweapons[0].UnlockStatus = true;
        #endregion
        //Master Volume
        #region Main Weapons Upgrades
        for (int j = 0; j < MainWeapons.Length; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".UpgradeIndex"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].UpgradeIndex = PlayerPrefs.GetInt("MainWeapon" + j + "Upgrade" + i + ".UpgradeIndex");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].UpgradeIndex = i;
                    PlayerPrefs.SetInt("MainWeapon" + j + "Upgrade" + i + ".UpgradeIndex", MainWeaponUpgs[j + (i * MainWeapons.Length)].UpgradeIndex);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".UpgradeCost"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].UpgradeCost = PlayerPrefs.GetInt("MainWeapon" + j + "Upgrade" + i + ".UpgradeCost");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].UpgradeCost = (i+1) * 100;
                    PlayerPrefs.SetInt("MainWeapon" + j + "Upgrade" + i + ".UpgradeCost", MainWeaponUpgs[j + (i * MainWeapons.Length)].UpgradeCost);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".WeaponIndex"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].WeaponIndex = PlayerPrefs.GetInt("MainWeapon" + j + "Upgrade" + i + ".WeaponIndex");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].WeaponIndex = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".ID");
                    PlayerPrefs.SetInt("MainWeapon" + j + "Upgrade" + i + ".WeaponIndex", MainWeaponUpgs[j + (i * MainWeapons.Length)].WeaponIndex);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".Damage"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].Damage = PlayerPrefs.GetInt("MainWeapon" + j + "Upgrade" + i + ".Damage");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].Damage = PlayerPrefs.GetInt("MainWeaponSlots" + i + ".Damage");
                    PlayerPrefs.SetInt("MainWeapon" + j + "Upgrade" + i + ".Damage", MainWeaponUpgs[j + (i * MainWeapons.Length)].Damage);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".Duration"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].Duration = PlayerPrefs.GetFloat("MainWeapon" + j + "Upgrade" + i + ".Duration");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].Duration = 5;
                    PlayerPrefs.SetFloat("MainWeapon" + j + "Upgrade" + i + ".Duration", MainWeaponUpgs[j + (i * MainWeapons.Length)].Duration);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".SlowFactor"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].SlowFactor = PlayerPrefs.GetFloat("MainWeapon" + j + "Upgrade" + i + ".SlowFactor");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].SlowFactor = 2;
                    PlayerPrefs.SetFloat("MainWeapon" + j + "Upgrade" + i + ".SlowFactor", MainWeaponUpgs[j + (i * MainWeapons.Length)].SlowFactor);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".ExtraTurrets"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].ExtraTurrets = PlayerPrefs.GetInt("MainWeapon" + j + "Upgrade" + i + ".ExtraTurrets");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].ExtraTurrets = 2;
                    PlayerPrefs.SetInt("MainWeapon" + j + "Upgrade" + i + ".ExtraTurrets", MainWeaponUpgs[j + (i * MainWeapons.Length)].ExtraTurrets);
                }
                if (PlayerPrefs.HasKey("MainWeapon" + j + "Upgrade" + i + ".FireRate"))
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].FireRate = PlayerPrefs.GetFloat("MainWeapon" + j + "Upgrade" + i + ".FireRate");
                }
                else
                {
                    MainWeaponUpgs[j + (i * MainWeapons.Length)].FireRate = 2;
                    PlayerPrefs.SetFloat("MainWeapon" + j + "Upgrade" + i + ".FireRate", MainWeaponUpgs[j + (i * MainWeapons.Length)].FireRate);
                }
            }
        }
        #endregion
        #region General
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
            Player.MaxWaveReached = 1;
            PlayerPrefs.SetInt(MaxWaveReachedRef, Player.MaxWaveReached);
        }
        if(PlayerPrefs.HasKey(HighestWaveStreakRef))
        {
            Player.HighestWaveStreak = PlayerPrefs.GetInt(HighestWaveStreakRef);
        }
        else
        {
            Player.HighestWaveStreak = 0;
            PlayerPrefs.SetInt(HighestWaveStreakRef, Player.HighestWaveStreak);
        }
        #endregion
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
                    Waves[z].WaveNumber = PlayerPrefs.GetInt("Wave" + z + ".Number");
                }
                else
                {
                    Waves[z].WaveNumber = j + 1;
                    PlayerPrefs.SetInt("Wave" + z + ".Number", j + 1);
                }
                if (PlayerPrefs.HasKey("Wave" + z + ".IsUnlocked"))
                {
                    Waves[z].Is_Unlocked = PlayerPrefs.GetInt("Wave" + z + ".IsUnlocked");
                }
                else
                {
                    Waves[z].Is_Unlocked = (z == 0 ? 1 : 0);
                    PlayerPrefs.SetInt("Wave" + z + ".IsUnlocked", Waves[z].Is_Unlocked);
                }
            }
        }
        #endregion
        #region Bundle data
        if(PlayerPrefs.HasKey("100CoinsPrice"))
        {
            Bundle.CoinPrice100 = PlayerPrefs.GetFloat("100CoinsPrice");
        }
        else
        {
            Bundle.CoinPrice100 = 0.1f;
            PlayerPrefs.SetFloat("100CoinsPrice",0.1f);
        }
        if (PlayerPrefs.HasKey("500CoinsPrice"))
        {
            Bundle.CoinPrice500 = PlayerPrefs.GetFloat("500CoinsPrice");
        }
        else
        {
            Bundle.CoinPrice500 = 0.4f;
            PlayerPrefs.SetFloat("500CoinsPrice", 0.4f);
        }
        if (PlayerPrefs.HasKey("1000CoinsPrice"))
        {
            Bundle.CoinPrice1000 = PlayerPrefs.GetFloat("1000CoinsPrice");
        }
        else
        {
            Bundle.CoinPrice1000 = 0.75f;
            PlayerPrefs.SetFloat("1000CoinsPrice", 0.75f);
        }
        if (PlayerPrefs.HasKey("2000CoinsPrice"))
        {
            Bundle.CoinPrice2000 = PlayerPrefs.GetFloat("2000CoinsPrice");
        }
        else
        {
            Bundle.CoinPrice2000 = 1.25f;
            PlayerPrefs.SetFloat("2000CoinsPrice", 1.25f);
        }
        if (PlayerPrefs.HasKey("4000CoinsPrice"))
        {
            Bundle.CoinPrice4000 = PlayerPrefs.GetFloat("4000CoinsPrice");
        }
        else
        {
            Bundle.CoinPrice4000 = 2.25f;
            PlayerPrefs.SetFloat("4000CoinsPrice", 2.25f);
        }
        if (PlayerPrefs.HasKey("10000CoinsPrice"))
        {
            Bundle.CoinPrice10000 = PlayerPrefs.GetFloat("10000CoinsPrice");
        }
        else
        {
            Bundle.CoinPrice10000 = 5f;
            PlayerPrefs.SetFloat("10000CoinsPrice", 5f);
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
        return MainWeapons[index].Unlocked;
    }
    public bool GetTempWeaponSlotStatus(int index)
    {
        return TempWeapons[index].Unlocked;
    }
    public List<int> GetUnlockedMainWeaponsIDs()
    {
        List<int> IDs = new List<int>();
        foreach (var item in MainWeapons)
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
        foreach (var item in TempWeapons)
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
        foreach (var item in MainWeapons)
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
        foreach (var item in TempWeapons)
        {
            if (item.Unlocked == false)
            {
                IDs.Add(item.ID);
            }
        }
        return IDs;

    }
    public int GetMainWeaponLevel(int index)
    {
        return MainWeapons[index].Level;
    }
    public int GetMainWeaponDamage(int index)
    {
        return MainWeapons[index].Damage;
    }
    public float GetMainWeaponFireRate(int index)
    {
        return MainWeapons[index].FireRate;
    }
   public int GetTempWeaponLevel(int index)
    {
        return TempWeapons[index].Level;
    }
    public List<TempWeaponsData> Get_InGameTempweaponsSlots()
    {
        return new List<TempWeaponsData>( m_InGameTempweapons);
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

    public void SetTempWeaponToSlot(int index,int _weaponID)
    {
        m_InGameTempweapons[index].WeaponID = _weaponID;
        OnDataChange();
    }
    public bool UnlockNewTempWeaponSlot()
    {
        for (int i = 0; i < m_InGameTempweapons.Length; i++)
        {
            if (m_InGameTempweapons[i].UnlockStatus == false)
            {
                m_InGameTempweapons[i].UnlockStatus = true;
                PlayerPrefs.SetInt("TempWeaponInGameSlots" + i + ".Unlocked", 1);
                OnDataChange();
                return true;
            }
        }
        return false;
    }

    public bool UnlockMainWeapon(int index)
    {
        if (MainWeapons[index].Unlocked == false)
        {
            PlayerPrefs.SetInt("MainWeaponSlots" + index + ".Unlocked", 1);
            MainWeapons[index].Unlocked = true;
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
        if (TempWeapons[index].Unlocked == false)
        {
            PlayerPrefs.SetInt("TempWeaponSlots" + index + ".Unlocked", 1);
            TempWeapons[index].Unlocked = true;
            OnDataChange();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMainWeaponStats(int index, int damage, float fireRate, float duration, float slowFactor, int extraTurrets)
    {
        MainWeapons[index].Damage = damage;
        PlayerPrefs.SetInt("MainWeaponSlots" + index + ".Damage", damage);
        MainWeapons[index].FireRate = fireRate;
        PlayerPrefs.SetFloat("MainWeaponSlots" + index + ".FireRate", fireRate);
        MainWeapons[index].Duration = duration;
        PlayerPrefs.SetFloat("MainWeaponSlots" + index + ".Duration", duration);
        MainWeapons[index].SlowFactor = slowFactor;
        PlayerPrefs.SetFloat("MainWeaponSlots" + index + ".SlowFactor", slowFactor);
        MainWeapons[index].ExtraTurrets = extraTurrets;
        PlayerPrefs.SetInt("MainWeaponSlots" + index + ".ExtraTurrets", extraTurrets);
        PlayerPrefs.Save();
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
    public void UpgradeMainWeapon(int index)
    {
        int lvl = GetMainWeaponLevel(index) +1;
        MainWeapons[index].Level += 1;
        PlayerPrefs.SetInt("MainWeaponSlots" + index + ".Level", MainWeapons[index].Level);
        int damage = MainWeaponUpgs[index + (MainWeapons.Length * lvl)].Damage;
        float fireRate = MainWeaponUpgs[index + (MainWeapons.Length * lvl)].FireRate;
        float duration = MainWeaponUpgs[index + (MainWeapons.Length * lvl)].Duration;
        float slowFactor = MainWeaponUpgs[index + (MainWeapons.Length * lvl)].SlowFactor;
        int extraTurrets = MainWeaponUpgs[index + (MainWeapons.Length * lvl)].ExtraTurrets;
        SetMainWeaponStats(index, damage, fireRate, duration, slowFactor, extraTurrets);
    }
    public void UpgradeTempWeapon(int index)
    {
        int lvl = GetTempWeaponLevel(index) +1;
        TempWeapons[index].Level += 1;
        PlayerPrefs.SetInt("TempWeaponSlots" + index + ".Level", TempWeapons[index].Level);
    }
}
