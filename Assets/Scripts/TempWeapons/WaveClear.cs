using UnityEngine;
using System.Collections;

public class WaveClear : MonoBehaviour {
    public int Damage = 1000;
    public int cost = 300;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
}
