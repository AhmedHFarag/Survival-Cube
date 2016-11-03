using UnityEngine;
using System.Collections;

public class Weapon2Laser : DefaultWeapon
{
    LaserBehaviourScript[] lasers;
    void Start()
    {
        lasers = GetComponentsInChildren<LaserBehaviourScript>();
    }
    public override void Fire()
    {
        foreach (var item in lasers)
        {

        }
    }
}