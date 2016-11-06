using UnityEngine;
using System.Collections;

public class UpgradeBuffs : MonoBehaviour {
    public bool _Speed;
    public int Speed;
    public bool _FireRate;
    public float FireRate;
    public bool _Shield;
    public bool _DeBuff=false;

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {

            Hit();
        }

    }
    public void Hit()
    {
        if (_DeBuff)
        {
            Player_Controller.Instance.DeBuffs();

        }
        else
        {
            Player_Controller.Instance.UpgradeBuffs(this);
        }
        Destroy(gameObject);
    }
}
