using UnityEngine;
using System.Collections;

public class WaveClear : TempWeapon
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
    public override void SelfInitialize(GameObject _obj)
    {
        transform.position = _obj.transform.position;
        gameObject.SetActive(true);
    }
}
