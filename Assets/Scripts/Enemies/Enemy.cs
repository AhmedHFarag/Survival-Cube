using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyBehaviour( GameObject Sender,int AddedScore);
    public static event EnemyBehaviour OnEnemyDie;

    public Transform Target;
    public float moveSpeed = 5;
    public int attackDamage = 0;
    public float lookatDisace = 1;
    public float DefaultHP = 200;
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
        myRigid.transform.LookAt(Target);
        //myRigid.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        myRigid.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    public virtual void TakeDamage(int damage)
    {

        HP -= damage;
        if (HP <= 0)
        {
            GameManager.Instance.SpawnItem(transform.position);
            Die();
        }
    }
    public void Die()
    {

        //update score
        GameManager.Instance.score += 10;
        StartCoroutine("death");
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (health)
        {
            health.enabled = false;
        }
        Explosion.Play();
        //raise event
        if (OnEnemyDie != null) { OnEnemyDie(this.gameObject,AddedScore); }
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            HP = 0;
            col.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage);
            Die();
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
