﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies_Manager : MonoBehaviour {
    public static Enemies_Manager Instance;

    #region PoolManager
    public PoolManager Pool_Manager;
    List<ObjectPool> Enemies_Pool = new List<ObjectPool>();
    #endregion

    #region SawnPoints
    public GameObject _SpawnPoints;
    [HideInInspector]
    public List<Transform> spawnPoints = new List<Transform>();
    #endregion

    #region EnemiesWaves
    int WaveNumber = 0;
    public List<MobWave> Waves = new List<MobWave>();

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
        Pool_Manager = new PoolManager();
    }
	void Start () {
        foreach (var item in _SpawnPoints.GetComponentsInChildren<Transform>())
        {
            spawnPoints.Add(item);
        }

        //Enemies_Pool.Add(Pool_Manager.CreatePool(eniemytype, 10, 20));

	}
	void FixedUpdate () {
        //Spawn(0, 1);
	}
    public void Spawn(int spawnPrefabIndex, int spawnPointIndex)
    {
        GameObject obj= Enemies_Pool[spawnPrefabIndex].GetObject();
        obj.transform.position=spawnPoints[spawnPointIndex].position;
        activeEnemies.Add(obj);
    }
    public void EnemyKilled(GameObject _obj)
    {
        activeEnemies.Remove(_obj);
        _obj.SetActive(false);
    }
}
