using UnityEngine;
using System.Collections;

public class Laser : TempWeapon
{
    LaserBehaviourScript[] lasers;
    void Awake()
    {
        lasers = GetComponentsInChildren<LaserBehaviourScript>();
        foreach (var item in lasers)
        {
            item.Damage = Damage;
        }
    }
}