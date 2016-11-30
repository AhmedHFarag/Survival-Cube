using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DefaultWeapon : MonoBehaviour
{
    public GameObject Bullet;
    public List<Transform> FirePositions;
    public Sprite UISprite;
    public int Cost = 200;

    public int BullectNumberInUse=20;

    public int maxBullectNumber=100;
    [HideInInspector]
    public ObjectPool bulletPool;
    protected GameObject currentbulletObj;
    protected float EllapsedTime = 0;
    public float _DefaultFireRate = 2;
    [HideInInspector]
    public float FireRate;

    public virtual void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Bullet, BullectNumberInUse, maxBullectNumber);
        FireRate = _DefaultFireRate;

    }
    void Update()
    {
        EllapsedTime += Time.deltaTime;
        //Fire Rate To be Deleted Need to be added in weapon class
        if (EllapsedTime > 1/FireRate)
        {
            EllapsedTime = 0;
            Fire();
        }
    }

    public virtual void Fire()
    {
        
            foreach (Transform FirePos in FirePositions)
            {
                currentbulletObj = bulletPool.GetObject();
                if (currentbulletObj)
                    {
                        currentbulletObj.transform.position = FirePos.position;
                        currentbulletObj.transform.forward = FirePos.forward;
                        currentbulletObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        currentbulletObj.SetActive(true);
                        currentbulletObj.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
                    }
            
            }
        //GameObject Shot = Instantiate(Bullet) as GameObject;
        //Shot.transform.position = FirePos1.position;
        //Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        ////Destroy(Shot, 1);
    }
}
