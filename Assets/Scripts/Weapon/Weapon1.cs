using UnityEngine;
using System.Collections;

public class Weapon1 : DefaultWeapon
{
    public Transform FirePos2;
    public Transform FirePos3;
    public override void Fire()
    {
        GameObject Shot = Instantiate(Bullet) as GameObject;
        Shot.transform.position = FirePos1.position;
        Shot.GetComponent<Rigidbody>().AddForce(FirePos1.forward * 5000);
        Destroy(Shot, 1);
        Shot = Instantiate(Bullet) as GameObject;
        Shot.transform.position = FirePos2.position;
        Shot.GetComponent<Rigidbody>().AddForce(FirePos2.forward * 5000);
        Destroy(Shot, 1);
        Shot = Instantiate(Bullet) as GameObject;
        Shot.transform.position = FirePos3.position;
        Shot.GetComponent<Rigidbody>().AddForce(FirePos3.forward * 5000);
        Destroy(Shot, 1);
    }
}