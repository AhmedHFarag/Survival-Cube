using UnityEngine;
using System.Collections;

public class DefaultWeapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos1;
    public int Cost = 200;

    public int BullectNumberInUse=20;

    public int maxBullectNumber=100;
    [HideInInspector]
    public ObjectPool bulletPool;
    private GameObject currentbulletObj;
    float EllapsedTime = 0;
    public float _DefaultFirRate = 2;
    [HideInInspector]
    public float FirRate;

    public virtual void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Bullet, BullectNumberInUse, maxBullectNumber);
        FirRate = _DefaultFirRate;

    }
    void Update()
    {
        EllapsedTime += Time.deltaTime;
        //Fire Rate To be Deleted Need to be added in weapon class
        if (EllapsedTime > 1/FirRate)
        {
            EllapsedTime = 0;
            Fire();
        }
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
