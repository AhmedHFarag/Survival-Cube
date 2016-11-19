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
}
