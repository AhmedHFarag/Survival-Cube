using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public Transform Target;
    public float moveSpeed = 0;
    public float attackDamage = 0;
    public float lookatDisace = 1;
    public float Health = 5;
    public float attackRange = 0;
    public Material materialColor1;
    public Material materialColor2;
    public float duration = 2.0F;
    private Rigidbody myRigid;
    private float Distance;
    public float Dampling=5f;
    private Renderer rend;
    
   
    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rend.material = materialColor1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Distance = Vector3.Distance(myRigid.transform.position, Target.transform.position);
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        if (Distance < 3)
        {
            rend.material.Lerp(materialColor1, materialColor2,lerp);
        }

        Attack();
    }
    public virtual void Patrol()
    {

    }
    public virtual void Attack()
    {
        myRigid.transform.LookAt(Target);
       //    myRigid.transform.Translate(Vector3.left * 5f * Time.deltaTime);
        myRigid.transform.Translate(Vector3.forward * Dampling * Time.deltaTime);
    }
    public virtual void TakeDamage()
    {

        Health -= 5;
    }
    public void Die()
    {
       
        Destroy(myRigid);
       
    }
    public void OnCollisionEnter(Collision col )
    {
        if (col.gameObject.tag == "Player")
        {
            Health = 0;
            Die();
        }
    }
}
