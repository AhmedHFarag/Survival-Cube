using UnityEngine;
using System.Collections;

public class UpgradeBuffs : MonoBehaviour {
    public bool _Damage;
    public float DamageMultiplier;
    public bool _Speed;
    public float Speed;
    public bool _FireRate;
    public float FireRate;
    public bool _Shield;
    public bool _ReverseControl = false;
    public bool Heal = false;
    public int HealAmount = 10;
    void Start()
    {
        StartCoroutine("Disappear");

    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {

            Hit();
        }

    }
    public void Hit()
    {
        if (_ReverseControl)
        {
            Player_Controller.Instance.ReverseControls();

        }
        else if(Heal)
        {
            Player_Controller.Instance.Heal(HealAmount);
        }
        else
        {
            Player_Controller.Instance.UpgradeBuffs(this);
        }
        Destroy(gameObject);
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
