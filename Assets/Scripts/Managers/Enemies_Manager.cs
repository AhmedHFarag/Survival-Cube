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
    List<List<ObjectPool>> Enemies_Pool = new List<List<ObjectPool>>();
    #endregion

    #region SawnPoints
    public GameObject _SpawnPoints;
    [HideInInspector]
    public List<Transform> spawnPoints = new List<Transform>();
    #endregion

    #region EnemiesWaves
    int WaveNumber = 0;
    int EnemieNumber = 0;
    int Enemiescount=0;
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
	void Start () {
        foreach (var item in _SpawnPoints.GetComponentsInChildren<Transform>().Skip(1))
        {
            spawnPoints.Add(item);
        }
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
	}
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime);

    }
    void FixedUpdate()
    {
        EllapsedTime += Time.deltaTime;
        if (WaveNumber < WavesData.Count)
        {
            if (EllapsedTime > WavesData[WaveNumber].Intensity)
            {
                EllapsedTime = 0;

                Spawn(WaveNumber, EnemieNumber);
                Enemiescount++;
                if (Enemiescount >= WavesData[WaveNumber].Enemies[EnemieNumber].Count)
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
    public bool Spawn(int Wavenumber,int EnemyNumber)
    {
        GameObject obj= Enemies_Pool[Wavenumber][EnemyNumber].GetObject();
        if (obj==null)
        {
            return false;
        }
        obj.transform.position=spawnPoints[Random.Range(0, spawnPoints.Count)].position;
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
