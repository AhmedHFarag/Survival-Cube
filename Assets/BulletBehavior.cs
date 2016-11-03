using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {
    public int Damage;
    public float Speed=2;
    public Rigidbody MyRigid;
	// Use this for initialization
    void OnEnable()
    {
    }
	void Start () {
	if(MyRigid==null)
        {
            MyRigid = gameObject.GetComponent<Rigidbody>();
        }
	}
void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            Debug.Log("enemy");
        }
    }
}
