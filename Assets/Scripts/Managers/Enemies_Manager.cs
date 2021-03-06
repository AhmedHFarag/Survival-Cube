﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemies_Manager : MonoBehaviour
{
    public static Enemies_Manager Instance;
    float EllapsedTime = 0;
    #region PoolManager
    List<List<ObjectPool>> Enemies_Pool = new List<List<ObjectPool>>();
    #endregion

    #region SawnPoints
    public GameObject _SpawnPoints;
    [HideInInspector]
    public List<Transform> spawnPoints = new List<Transform>();
    #endregion
    public GameObject Bullet;
    [HideInInspector]
    public ObjectPool BulletPool;

    #region EnemiesWaves
    int CurrentWaveNumber = 0;//
    int CurrentEnemieNumber = 0;
    int Currentlevel = 1;//
    int Enemiescount = 0;
    bool SpawnEnabled = false;
    public List<Wave> WavesData = new List<Wave>();

    [HideInInspector]
    public List<GameObject> activeEnemies = new List<GameObject>();
    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }
    }
    void Start()
    {
        BulletPool = GameManager.Instance.CreatePool(Bullet, 25, 50);
        foreach (var item in _SpawnPoints.GetComponentsInChildren<Transform>().Skip(1))
        {
            spawnPoints.Add(item);
        }
        CurrentWaveNumber = GameManager.Instance.CurrentWaveNumber;//
        Currentlevel = GameManager.Instance.Currentlevel;//
        int i = 0;
        foreach (Wave wave in WavesData)
        {
            Enemies_Pool.Add(new List<ObjectPool>());
            foreach (WaveEnemies item in wave.Enemies)
            {
                Enemies_Pool[i].Add(GameManager.Instance.Pool_Manager.CreatePool(item.Prefab, item.Count, item.Count));
            }
            i++;
        }
        StartCoroutine("New_Wave");
        Enemiescount = WavesData[CurrentWaveNumber].Enemies[CurrentEnemieNumber].Count;
        Enemy.OnEnemyDie += EnemyDeath;
        
    }

    void FixedUpdate()
    {
        EllapsedTime += Time.deltaTime;
        if (CurrentWaveNumber < WavesData.Count)
        {
            if (EllapsedTime > 1/(WavesData[CurrentWaveNumber].Intensity) && SpawnEnabled)
            {
                EllapsedTime = 0;
                if (Spawn())
                {
                    ///Enemiescount++;
                }
                else
                {
                    if (activeEnemies.Count <= 0)
                    {
                        CurrentWaveNumber++;
                        GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_here_they_come_again, 300, 1);
                        if (CurrentWaveNumber < WavesData.Count)
                        {
                            CurrentEnemieNumber = 0;
                            Enemiescount = WavesData[CurrentWaveNumber].Enemies[CurrentEnemieNumber].Count;
                            SpawnEnabled = false;
                            StartCoroutine("New_Wave");
                        }
                        else
                        {
                            // Start from zero again
                            CurrentWaveNumber = 0;
                            //increase defficulity
                            Currentlevel++;
                            if(Currentlevel==2)
                            {
                                GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_same_but_different, 500);
                            }
                        }
                        if (DataHandler.Instance.GetLevelUnlocked((Currentlevel - 1) * 10 + CurrentWaveNumber) == 0)
                        {
                            DataHandler.Instance.SetWaveUnlocked((Currentlevel - 1) * 10 + CurrentWaveNumber);
                            DataHandler.Instance.SetMaxWaveReached((Currentlevel - 1) * 10 + CurrentWaveNumber);
                        }
                        
                    }
                }
            }
        }
    }

    public bool Spawn()
    {
        if (Enemiescount > 0)
        {
            GameObject obj = Enemies_Pool[CurrentWaveNumber][CurrentEnemieNumber].GetObject();
            if (obj == null)
            {
                //Debug.Log("Enemies 5elso Min EL Pool");
                return false;
            }
            Enemiescount--;
            obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            activeEnemies.Add(obj);
            return true;
        }
        else
        {
            if (CurrentEnemieNumber < WavesData[CurrentWaveNumber].Enemies.Length - 1)
            {
                CurrentEnemieNumber++;
                Enemiescount = WavesData[CurrentWaveNumber].Enemies[CurrentEnemieNumber].Count;
                return Spawn();
            }
            else
            {
                return false;
            }
        }
    }
    IEnumerator New_Wave()
    {
        //Debug.Log("NewWave"+CurrentWaveNumber);
        GameManager.Instance.NewWavStarted();
        yield return new WaitForSeconds(5);
        SpawnEnabled = true;
    }
    public void EnemyKilled(GameObject _obj)
    {
        activeEnemies.Remove(_obj);
    }
    
    void EnemyDeath(GameObject Enemy, int Score, int Coins, bool collideWithPlayer)
    {
        if (!collideWithPlayer)
        {
            DataHandler.Instance.AddInGameScore(Score);
            DataHandler.Instance.AddInGameCoins(Coins);
            //Spawn Reward Item
            GameManager.Instance.SpawnItem(Enemy.transform.position);
        }
        EnemyKilled(Enemy);
    }
    public int GetCurrentLevel()
    {
        return Currentlevel;
    }
    public int GetCurrentWave()
    {
        return CurrentWaveNumber;
    }
    public void SetCurrentLevel(int level)
    {
        Currentlevel = level;
    }
    public void SetCurrentWave(int wave)
    {
        CurrentWaveNumber = wave;
    }
    void OnDestroy()
    {
        Enemy.OnEnemyDie -= EnemyDeath;
    }
   public void ResetWaveAndLevel()
    {
        Currentlevel = 1;
        CurrentWaveNumber = 0;
    }
}
