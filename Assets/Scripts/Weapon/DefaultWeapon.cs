using UnityEngine;
using System.Collections;

public class DefaultWeapon : MonoBehaviour {
    public GameObject Bullet;
    public Transform FirePos1;

    public virtual void Fire()
    {
        GameObject Shot = Instantiate(Bullet) as GameObject;
        Shot.transform.position = FirePos1.position;
        Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        Destroy(Shot, 1);
    }
}
