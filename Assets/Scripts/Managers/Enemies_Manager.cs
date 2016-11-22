using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemies_Manager : MonoBehaviour
{
    public static Enemies_Manager Instance;
    public bool Show_FPS = false;
    float deltaTime = 0.0f;

    float EllapsedTime = 0;
    #region PoolManager
    List<List<ObjectPool>> Enemies_Pool = new List<List<ObjectPool>>();
    #endregion

    #region SawnPoints
    public GameObject _SpawnPoints;
    [HideInInspector]
    public List<Transform> spawnPoints = new List<Transform>();
    #endregion

    #region EnemiesWaves
    int CurrentWaveNumber = 0;
    int CurrentEnemieNumber = 0;
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
        StartCoroutine("New_Wave");
        Enemy.OnEnemyDie += EnemyDeath;

    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime);

    }
    void FixedUpdate()
    {
        EllapsedTime += Time.deltaTime;
        if (CurrentWaveNumber < WavesData.Count)
        {
            if (EllapsedTime > WavesData[CurrentWaveNumber].Intensity && SpawnEnabled)
            {
                EllapsedTime = 0;
                if (Spawn())
                {
                    Enemiescount++;
                }
                else
                {
                    
                    if (activeEnemies.Count<=0)
                    {
                        CurrentWaveNumber++;
                        CurrentEnemieNumber = 0;
                        SpawnEnabled = false;
                        StartCoroutine("New_Wave");
                        Enemiescount = 0;
                    }
                }
            }
        }
        else
        {
           // Debug.Log("Spawn Ended");
            //CurrentWaveNumber = 0;/////Just For Test Remove this
        }
    }

    public bool Spawn()
    {
        if (WavesData[CurrentWaveNumber].Enemies[CurrentEnemieNumber].Count > 0)
        {
            GameObject obj = Enemies_Pool[CurrentWaveNumber][CurrentEnemieNumber].GetObject();
            if (obj == null)
            {
                //Debug.Log("Enemies 5elso Min EL Pool");
                return false;
            }
            WavesData[CurrentWaveNumber].Enemies[CurrentEnemieNumber].Count--;
            obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            activeEnemies.Add(obj);
            return true;
        }
        else
        {
            if (CurrentEnemieNumber < WavesData[CurrentWaveNumber].Enemies.Length - 1)
            {
                CurrentEnemieNumber++;
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
    void EnemyDeath(GameObject Enemy, int Score, bool collideWithPlayer)
    {
        if (!collideWithPlayer)
            //   GameManager.Instance.score += Score;
            DataHandler.Instance.AchievementScore += Score;

        EnemyKilled(Enemy);
    }
    void OnDestroy()
    {
        Enemy.OnEnemyDie -= EnemyDeath;
    }
}
