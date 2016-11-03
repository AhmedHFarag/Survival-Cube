using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {
    int damage;
    float speed=2;
    public Rigidbody MyRigid;
	// Use this for initialization
    void OnEnable()
    {
        MyRigid.velocity = transform.forward * speed;
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
            other.gameObject.GetComponent<Enemy>().TakeDamage();
        }
    }
}
