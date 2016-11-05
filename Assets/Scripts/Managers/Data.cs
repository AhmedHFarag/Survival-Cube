using UnityEngine;
using System;

[Serializable]
public struct Wave {
    public WaveEnemies[] Enemies;
    public float Intensity;
}
[Serializable]
public struct WaveEnemies
{
    //public enum EnemyType
    //{
    //    Mobs,
    //    Boss
    //}
    //public EnemyType Type;
    public int Count;
    public GameObject Prefab;
}
