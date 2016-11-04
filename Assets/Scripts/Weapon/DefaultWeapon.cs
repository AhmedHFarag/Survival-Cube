using UnityEngine;
using System.Collections;

public class DefaultWeapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos1;

    public int BullectNumberInUse;

    public int maxBullectNumber;

    private ObjectPool bulletPool;
    private GameObject currentbulletObj;

    public virtual void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Bullet, BullectNumberInUse, maxBullectNumber);
    }

    public virtual void Fire()
    {
        currentbulletObj = bulletPool.GetObject();
        currentbulletObj.transform.position = FirePos1.position;
        currentbulletObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentbulletObj.SetActive(true);
        currentbulletObj.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);

        //GameObject Shot = Instantiate(Bullet) as GameObject;
        //Shot.transform.position = FirePos1.position;
        //Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        ////Destroy(Shot, 1);
    }
}
