using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaveGenerator : MonoBehaviour
{
    public GameObject SpawnPoints;
    [HideInInspector] public List<Transform> spawnPoints = new List<Transform>();
    public GameObject[] EnemyTypesPrefab;

    public int EnemyCount;
    public float spawnWait;
    public float waveWait;
    public int WaveNumber; // just for testing
    public bool increaseDifficulty;

    List<List<ObjectPool>> Enemies_Pool = new List<List<ObjectPool>>();
    [HideInInspector] public List<GameObject> activeEnemies = new List<GameObject>();



    void Start()
    {
         WaveNumber=0;
        foreach (var item in SpawnPoints.GetComponentsInChildren<Transform>().Skip(1))
        {
            spawnPoints.Add(item);
                     
        }
        StartCoroutine(SpawnWaves());
        Enemy.OnEnemyDie += EnemyDeath;

    }
    void Update()
    {
        int RandomEnemy = Random.Range(0, EnemyTypesPrefab.Length);
        Enemies_Pool.Add(new List<ObjectPool>());
        for (int i = 0; i < 4; i++)
        {
            Enemies_Pool[0].Add(GameManager.Instance.Pool_Manager.CreatePool(EnemyTypesPrefab[i], EnemyCount, EnemyCount));
        }


    }

    IEnumerator SpawnWaves()
    {
 
        while (true)
        {
            if (activeEnemies.Count < 1)
            {
                GameManager.Instance.NewWavStarted();
                yield return new WaitForSeconds(waveWait);
                WaveNumber += 1;
                if (increaseDifficulty)
                {
                    EnemyCount += 1;
                }
                for (int i = 0; i < EnemyCount; i++)
                {
                    switch (WaveNumber)
                    {
                        #region Introducion to types enemies first  waves are fixed for the player before he gets into the game
                        case 1:
                            GameObject obj1 = Enemies_Pool[0][1].GetObject();
                            if (obj1 == null)
                            {
                                yield return false;
                            }
                            else
                            {
                                obj1.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                                activeEnemies.Add(obj1); // to check if there are any active enemies to start the new waves
                            }
                            break;
                        case 2:
                            GameObject obj2 = Enemies_Pool[0][2].GetObject();
                            if (obj2 == null)
                            {
                                yield return false;
                            }
                            else
                            {
                                obj2.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                                activeEnemies.Add(obj2); // to check if there are any active enemies to start the new waves
                            }
                            break;
                        case 3:
                            GameObject obj3 = Enemies_Pool[0][3].GetObject();
                            if (obj3 == null)
                            {
                                yield return false;
                            }
                            else
                            {
                                obj3.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                                activeEnemies.Add(obj3); // to check if there are any active enemies to start the new waves
                            }
                            break;
                        case 4:
                            GameObject obj4 = Enemies_Pool[0][4].GetObject();
                            if (obj4 == null)
                            {
                                yield return false;
                            }
                            else
                            {
                                obj4.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                                activeEnemies.Add(obj4); // to check if there are any active enemies to start the new waves
                            }
                            break;
                        #endregion 
                        default:
                            GameObject obj = Enemies_Pool[0][i].GetObject();
                            if (obj == null)
                            {
                                yield return false;
                            }
                            else
                            {
                                obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                                activeEnemies.Add(obj); // to check if there are any active enemies to start the new waves
                            }

                            increaseDifficulty = true;

                            if (WaveNumber > 35)// this section if the player reached level 35 the enemies will keep coming endlessly
                            {                   // the only way to move to the next level is to clear them all before another enemy spawns
                                                // using any kind of powerful wepons
                                EnemyCount += 1;
                            }
                            break;
                         
                    }
                    
                    yield return new WaitForSeconds(spawnWait); // wait few seconds before spawn new enemy
                }
            }
            yield return new WaitForSeconds(1);

        }

        }
  
    public void EnemyKilled(GameObject _obj)
    {
        activeEnemies.Remove(_obj);
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
  

