using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemies_Manager : MonoBehaviour {
    public static Enemies_Manager Instance;
    public bool Show_FPS = false;
    float deltaTime = 0.0f;

    float EllapsedTime =0;
    #region PoolManager
    List<ObjectPool> Enemies_Pool = new List<ObjectPool>();
    #endregion

    #region SawnPoints
    public GameObject _SpawnPoints;
    [HideInInspector]
    public List<Transform> spawnPoints = new List<Transform>();
    #endregion

    #region EnemiesWaves
    int WaveNumber = 0;
    int Enemiescount=0;
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
        GameManager.Instance.Pool_Manager = new PoolManager();
    }
	void Start () {
        foreach (var item in _SpawnPoints.GetComponentsInChildren<Transform>().Skip(1))
        {
            spawnPoints.Add(item);
        }
        foreach (var item in Waves)
        {
            Enemies_Pool.Add(GameManager.Instance.Pool_Manager.CreatePool(item.Prefab, item.Count, item.Count));
        }
	}
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime);

    }
    void FixedUpdate()
    {
        EllapsedTime += Time.deltaTime;
        if (WaveNumber < Waves.Count)
        {
            if (EllapsedTime > Waves[WaveNumber].Intensity)
            {
                EllapsedTime = 0;

                Spawn(WaveNumber, Random.Range(0, spawnPoints.Count));
                Enemiescount++;
                if (Enemiescount >= Waves[WaveNumber].Count)
                {
                    WaveNumber++;
                    Enemiescount = 0;
                }
            }
        }
        else
        {
            Debug.Log("Spawn Ended");
        }
    }
    public bool Spawn(int spawnPrefabIndex, int spawnPointIndex)
    {
        GameObject obj= Enemies_Pool[spawnPrefabIndex].GetObject();
        if (obj==null)
        {
            return false;
        }
        obj.transform.position=spawnPoints[spawnPointIndex].position;
        activeEnemies.Add(obj);
        return true;
    }
    public void EnemyKilled(GameObject _obj)
    {
        activeEnemies.Remove(_obj);
        _obj.SetActive(false);
    }
    void OnGUI()
    {
        if (Show_FPS)
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 4 / 100;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }

}
