using UnityEngine;
using System.Collections;

public class UpgradeWeapon : MonoBehaviour {

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {

            Player_Controller.Instance.UpgradeWeapon();
            Destroy(gameObject);
        }

    }
}
