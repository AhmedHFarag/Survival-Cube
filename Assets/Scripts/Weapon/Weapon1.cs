using UnityEngine;
using System.Collections;

public class Weapon1 : DefaultWeapon
{
    public Transform FirePos2;
    public Transform FirePos3;
    public int price = 100;
    private ObjectPool bulletPool;

    GameObject currentbulletObj1;
    GameObject currentbulletObj2;
    GameObject currentbulletObj3;
    public override void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Bullet, BullectNumberInUse, maxBullectNumber);
    }

    public override void Fire()
    {

        currentbulletObj1 = bulletPool.GetObject();
        if (currentbulletObj1)
        {
            currentbulletObj1.SetActive(true);
            currentbulletObj1.transform.position = FirePos1.position;
            currentbulletObj1.GetComponent<Rigidbody>().AddForce(FirePos1.forward * 5000);
        }

        currentbulletObj2 = bulletPool.GetObject();
        if (currentbulletObj2)
        {
            currentbulletObj2.SetActive(true);
            currentbulletObj2.transform.position = FirePos2.position;
            currentbulletObj2.GetComponent<Rigidbody>().AddForce(FirePos2.forward * 5000);
        }

        currentbulletObj3 = bulletPool.GetObject();
        if (currentbulletObj3)
        {
            currentbulletObj3.SetActive(true);
            currentbulletObj3.transform.position = FirePos3.position;
            currentbulletObj3.GetComponent<Rigidbody>().AddForce(FirePos3.forward * 5000);
        }
        //GameObject Shot = Instantiate(Bullet) as GameObject;
        //Shot.transform.position = FirePos1.position;
        //Shot.GetComponent<Rigidbody>().AddForce(FirePos1.forward * 5000);
        //Destroy(Shot, 1);
        //Shot = Instantiate(Bullet) as GameObject;
        //Shot.transform.position = FirePos2.position;
        //Shot.GetComponent<Rigidbody>().AddForce(FirePos2.forward * 5000);
        //Destroy(Shot, 1);
        //Shot = Instantiate(Bullet) as GameObject;
        //Shot.transform.position = FirePos3.position;
        //Shot.GetComponent<Rigidbody>().AddForce(FirePos3.forward * 5000);
        //Destroy(Shot, 1);
    }
}