using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemies_Manager : MonoBehaviour {
    public static Enemies_Manager Instance;

    float EllapsedTime =0;
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
    int intensity = 1;
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
        foreach (var item in _SpawnPoints.GetComponentsInChildren<Transform>().Skip(1))
        {
            spawnPoints.Add(item);
        }
        foreach (var item in Waves)
        {
            Enemies_Pool.Add(Pool_Manager.CreatePool(item.Prefab, item.Count, item.Count));
        }
	}
	void FixedUpdate () {
        EllapsedTime += Time.deltaTime;
        if (EllapsedTime>intensity)
        {
            EllapsedTime = 0;
            Spawn(0,Random.Range(0,4));
        }
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
