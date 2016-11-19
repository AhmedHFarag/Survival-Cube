using UnityEngine;
using System.Collections;

public class SpreadShot : DefaultWeapon
{
    public override void Fire()
    {
        foreach (var pos in FirePositions)
        {
            currentbulletObj = bulletPool.GetObject();
            if (currentbulletObj)
            {
                currentbulletObj.transform.position = pos.position;
                currentbulletObj.transform.forward = pos.forward;
                currentbulletObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                currentbulletObj.SetActive(true);
                currentbulletObj.GetComponent<Rigidbody>().AddForce(pos.forward * 2000);
            }
        }
    }
}