using UnityEngine;
using System.Collections;

public class UpgradeWeapon : MonoBehaviour {

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {

            Hit();
        }

    }
    public void Hit()
    {
        Player_Controller.Instance.UpgradeWeapon(1);
        Destroy(gameObject);
    }
}
