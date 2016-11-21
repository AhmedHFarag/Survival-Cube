using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

    public int Damage;
    void Update()
    {

        Destroy(gameObject, 2);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
            
        }
        
    }
}
