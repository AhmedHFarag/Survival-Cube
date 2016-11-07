using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyBehaviour(GameObject Sender, int AddedScore, bool isCollidedWithPlayer);
    public static event EnemyBehaviour OnEnemyDie;

    public Transform Target;
    public float moveSpeed = 5;
    public int attackDamage = 0;
    public float lookatDisace = 1;
    public float DefaultHP = 200;
    public int Value = 20;
    float HP;
    public float attackRange = 0;
    public Material materialColor1;
    public Material materialColor2;
    public ParticleSystem Explosion;
    public float duration = 2.0F;
    public int AddedScore = 10;
    public Slider healthBar;

    private Rigidbody myRigid;
    private float Distance;

    private Canvas health;

    float MyArea = 5;
    float Sep_Force =2;
    float Coh_Force = 1;
    float AlignForce = 1;
    void OnEnable()
    {
        HP = DefaultHP;
        health = gameObject.GetComponentInChildren<Canvas>() ? gameObject.GetComponentInChildren<Canvas>() : null;
        healthBar.maxValue = DefaultHP;
        healthBar.value = DefaultHP;
        health.enabled = true;
    }

    // Use this for initialization
    void Start()
    {

        Explosion = gameObject.GetComponentInChildren<ParticleSystem>();

        myRigid = GetComponent<Rigidbody>();
        Target = Player_Controller.Instance.GetComponent<Transform>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Distance = Vector3.Distance(myRigid.transform.position, Target.transform.position);
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        if (healthBar)
        {
            healthBar.value = HP;
        }

        Attack();
    }
    
    public virtual void Patrol()
    {

    }
    public virtual void Attack()
    {
        //myRigid.transform.LookAt(Target);
        //myRigid.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        //myRigid.transform.Translate((Vector3.forward+ Separation()) * moveSpeed * Time.deltaTime);
        myRigid.velocity = Truncate(myRigid.velocity + ToPlayer() + Separation()+ Alignment()+ Cohesion(), moveSpeed);
    }
    Vector3 Separation()
    {
        Vector3 SteeringForce = Vector3.zero;
        Vector3 PushForce = Vector3.zero;
        foreach (GameObject Boid in Enemies_Manager.Instance.activeEnemies)
        {
            if (Boid != this)
            {
                if (Vector3.Distance(myRigid.position, Boid.GetComponent<Rigidbody>().position) < MyArea)
                {
                    //SetUp Push Force
                    PushForce = myRigid.position - Boid.GetComponent<Rigidbody>().position;
                    SteeringForce += PushForce;

                }
            }
        }
        SteeringForce = SteeringForce.normalized * Sep_Force;
        return SteeringForce;
    }
    Vector3 Cohesion()
    {
        Vector3 CM = Vector3.zero;
        Vector3 SteeringForce = Vector3.zero;
        int Counter = 0;
        foreach (GameObject Boid in Enemies_Manager.Instance.activeEnemies)
        {
            if (Boid != this)
            {
                CM += Boid.GetComponent<Rigidbody>().position;
                Counter++;
            }
        }
        SteeringForce = (CM / Counter) - myRigid.position;
        SteeringForce = SteeringForce.normalized * Coh_Force;
        return SteeringForce;
    }
    Vector3 Alignment()
    {
        Vector3 CM = Vector3.zero;
        Vector3 SteeringForce = Vector3.zero;
        int Counter = 0;
        foreach (GameObject Boid in Enemies_Manager.Instance.activeEnemies)
        {
            if (Boid != this)
            {
                CM += Boid.GetComponent<Rigidbody>().velocity;
                Counter++;
            }
        }
        SteeringForce = (CM / Counter) - myRigid.velocity;
        SteeringForce = SteeringForce.normalized * AlignForce;
        return SteeringForce;
    }
    Vector3 ToPlayer()
    {
        Vector3 V_Des = (Target.GetComponent<Rigidbody>().position - myRigid.position).normalized * moveSpeed;
        Vector3 SteeringForce = (V_Des - myRigid.velocity);
        return SteeringForce;
    }
    Vector3 Truncate(Vector3 val, float Max)
    {
        if (val.magnitude > Max)
            return val.normalized * Max;
        return val;
    }
    public virtual void TakeDamage(int damage)
    {

        HP -= damage;
        if (HP <= 0)
        {
            GameManager.Instance.SpawnItem(transform.position);
            GameManager.Instance.InGameCoins += Value;
            Die();
        }
    }
    public void Die(bool isCollidedWithEnemy = false)
    {
        //if (isCollidedWithEnemy)
        //{
        //    //update score
        //    GameManager.Instance.score += 10;
        //}
        StartCoroutine("death");
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (health)
        {
            health.enabled = false;
        }
        Explosion.Play();
        //raise event
        if (OnEnemyDie != null) { OnEnemyDie(this.gameObject, AddedScore, isCollidedWithEnemy); }
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            HP = 0;
            col.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage);
            Die(true);
        }

    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Explosion.Stop();
        gameObject.SetActive(false);
    }
}
