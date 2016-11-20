using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour
{

    struct WeaponsData
    {
      public int WeaponID;
      public float WeaponStatus;
      public float WeaponCost;
      public float WeaponDamage;
    }
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
    public int AcivementScore;
    [HideInInspector]
    public int BestScore;
    [HideInInspector]
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
            weapon1.WeaponStatus = PlayerPrefs.GetFloat("weapon1.WeaponStatus");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon1.WeaponStatus", 5);
            weapon1.WeaponStatus = 5;
        }
        if(PlayerPrefs.HasKey("weapon1.WeaponCost"))
        {
            weapon1.WeaponCost = PlayerPrefs.GetFloat("weapon1.WeaponCost");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon1.WeaponCost", 30);
            weapon1.WeaponCost = 30;
        }
        if (PlayerPrefs.HasKey("weapon1.WeaponDamage"))
        {
            weapon1.WeaponDamage = PlayerPrefs.GetFloat("weapon1.WeaponDamage");
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
            weapon2.WeaponStatus = PlayerPrefs.GetFloat("weapon2.WeaponStatus");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon2.WeaponStatus", 5);
            weapon2.WeaponStatus = 5;
        }
        if (PlayerPrefs.HasKey("weapon2.WeaponCost"))
        {
            weapon2.WeaponCost = PlayerPrefs.GetFloat("weapon2.WeaponCost");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon2.WeaponCost", 30);
            weapon2.WeaponCost = 30;
        }
        if (PlayerPrefs.HasKey("weapon2.WeaponDamage"))
        {
            weapon2.WeaponDamage = PlayerPrefs.GetFloat("weapon2.WeaponDamage");
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
            weapon3.WeaponStatus = PlayerPrefs.GetFloat("weapon3.WeaponStatus");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon3.WeaponStatus", 5);    
            weapon3.WeaponStatus = 5;
        }
        if (PlayerPrefs.HasKey("weapon3.WeaponCost"))
        {
            weapon3.WeaponCost = PlayerPrefs.GetFloat("weapon3.WeaponCost");
        }
        else
        {
            PlayerPrefs.SetFloat("weapon3.WeaponCost", 30);
            weapon3.WeaponCost = 30;
        }
        if (PlayerPrefs.HasKey("weapon3.WeaponDamage"))
        {
            weapon3.WeaponDamage = PlayerPrefs.GetFloat("weapon3.WeaponDamage");
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
        inGameCoins = 0;
        AcivementScore = 0;
        SavePlayerPrefsData();

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

        PlayerPrefs.SetInt("weapon1.WeaponID", weapon1.WeaponID);
        PlayerPrefs.SetFloat("weapon1.WeaponStatus", weapon1.WeaponStatus);
        PlayerPrefs.SetFloat("weapon1.WeaponCost", weapon1.WeaponCost);
        PlayerPrefs.SetFloat("weapon1.WeaponDamage", weapon1.WeaponDamage);

        PlayerPrefs.SetInt("weapon2.WeaponID", weapon2.WeaponID);
        PlayerPrefs.SetFloat("weapon2.WeaponStatus", weapon2.WeaponStatus);
        PlayerPrefs.SetFloat("weapon2.WeaponCost", weapon2.WeaponCost);
        PlayerPrefs.SetFloat("weapon2.WeaponDamage", weapon2.WeaponDamage);

        PlayerPrefs.SetInt("weapon3.WeaponID", weapon3.WeaponID);
        PlayerPrefs.SetFloat("weapon3.WeaponStatus", weapon3.WeaponStatus);
        PlayerPrefs.SetFloat("weapon3.WeaponCost", weapon3.WeaponCost);
        PlayerPrefs.SetFloat("weapon3.WeaponDamage", weapon3.WeaponDamage);

        PlayerPrefs.Save();
    }
    #region Getters
    public int GetPlayerCoins()
    {
        return playerCoins;
    }
    public string GetPlayerCoinsstr()
    {
        return playerCoins.ToString();
    }

    public string GetPlayerName()
    {
        return PlayerName;
    }

    public int  GetWaveNumber()
    {
        return WaveNo;
    }
    public int GetBestScore()
    {
        return BestScore;
    }
    public string GetBestScoreStr()
    {
        return BestScore.ToString();
    }
    public int GetAcivementScore()
    {
        return AcivementScore;
    }
    public int GetWeapon1ID()
    {
        return weapon1.WeaponID;
    }
    public float GetWeapon1Status()
    {
        return weapon1.WeaponStatus;
    }
    public float GetWeapon1Cost()
    {
        return weapon1.WeaponCost;
    }
    public float GetWeapon1Damage()
    {
        return weapon1.WeaponDamage;
    }

    public int GetWeapon2ID()
    {
        return weapon2.WeaponID;
    }
    public float GetWeapon2Status()
    {
        return weapon2.WeaponStatus;
    }
    public float GetWeapon2Cost()
    {
        return weapon2.WeaponCost;
    }
    public float GetWeapon2Damage()
    {
        return weapon2.WeaponDamage;
    }
    public int GetWeapon3ID()
    {
        return weapon3.WeaponID;
    }
    public float GetWeapon3Status()
    {
        return weapon3.WeaponStatus;
    }
    public float GetWeapon3Cost()
    {
        return weapon3.WeaponCost;
    }
    public float GetWeapon3Damage()
    {
        return weapon3.WeaponDamage;
    }
    #endregion
    #region Setters
    public void SetPlayerCoins(int _PlayerCoins)
    {
        playerCoins = _PlayerCoins;
        SavePlayerPrefsData();
    }

    public void SetPlayerName(string _PlayerName)
    {
        PlayerName = _PlayerName;
        SavePlayerPrefsData();
    }

    public void SetWaveNumber(int _WaveNo)
    {   
        WaveNo = _WaveNo;
        SavePlayerPrefsData();
    }
    public void SetBestScore(int _BestScore)
    {
        BestScore = _BestScore;
        SavePlayerPrefsData();
    }
    public void SetAcivementScore(int _AchivementScore)
    {
        AcivementScore = _AchivementScore;
        SavePlayerPrefsData();
    }



    public void SetWeapon1ID(int _weaponID)
    {
        weapon1.WeaponID = _weaponID;
        SavePlayerPrefsData();
    }
    public void SetWeapon1Status(float _weaponStatus)
    {
        weapon1.WeaponStatus = _weaponStatus;
        SavePlayerPrefsData();
    }
    public void SetWeapon1Cost(float _weaponCost)
    {
        weapon1.WeaponCost = _weaponCost;
        SavePlayerPrefsData();
    }
    public void SetWeapon1Damage(float _weaponDamage)
    {
        weapon1.WeaponDamage = _weaponDamage;
        SavePlayerPrefsData();
    }

    public void SetWeapon2ID(int _weaponID)
    {
        weapon2.WeaponID = _weaponID;
        SavePlayerPrefsData();
    }
    public void SetWeapon2Status(float _weaponStatus)
    {
        weapon2.WeaponStatus = _weaponStatus;
        SavePlayerPrefsData();
    }
    public void SetWeapon2Cost(float _weaponCost)
    {
        weapon2.WeaponCost = _weaponCost;
        SavePlayerPrefsData();
    }
    public void SetWeapon2Damage(float _weaponDamage)
    {
        weapon2.WeaponDamage = _weaponDamage;
        SavePlayerPrefsData();
    }



    public void SetWeapon3ID(int _weaponID)
    {
        weapon3.WeaponID = _weaponID;
        SavePlayerPrefsData();
    }
    public void SetWeapon3Status(float _weaponStatus)
    {
        weapon3.WeaponStatus = _weaponStatus;
        SavePlayerPrefsData();
    }
    public void SetWeapon3Cost(float _weaponCost)
    {
        weapon3.WeaponCost = _weaponCost;
        SavePlayerPrefsData();
    }
    public void SetWeapon3Damage(float _weaponDamage)
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
