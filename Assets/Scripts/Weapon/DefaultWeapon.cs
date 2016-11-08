using UnityEngine;
using System.Collections;

public class DefaultWeapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos1;
    public int Cost = 200;

    public int BullectNumberInUse;

    public int maxBullectNumber;
    [HideInInspector]
    public ObjectPool bulletPool;
    private GameObject currentbulletObj;

    public virtual void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Bullet, BullectNumberInUse, maxBullectNumber);
    }

    public virtual void Fire()
    {
        currentbulletObj = bulletPool.GetObject();
        if (currentbulletObj)
        {
            currentbulletObj.transform.position = FirePos1.position;
            currentbulletObj.transform.forward = FirePos1.forward;
            currentbulletObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentbulletObj.SetActive(true);
            currentbulletObj.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        }
        //GameObject Shot = Instantiate(Bullet) as GameObject;
        //Shot.transform.position = FirePos1.position;
        //Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        ////Destroy(Shot, 1);
    }
}
