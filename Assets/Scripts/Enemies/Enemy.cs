using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Ranged
    }
    public delegate void EnemyBehaviour(GameObject Sender, int AddedScore,int AddedCoins, bool isCollidedWithPlayer);
    public static event EnemyBehaviour OnEnemyDie;

    public EnemyType Type=EnemyType.Melee;
    Transform Target;
    public float DefaultMoveSpeed=5;
    float moveSpeed;    
    public int DefaultAttackDamage = 50;
    int AttackDamage;
    public float DefaultHP = 200;
    public int AddedCoins = 20;
    public float Range;
    float HP;
    ParticleSystem Explosion;
    public int AddedScore = 10;
    public Slider healthBar;
    public bool AvBullets = false;
    private Rigidbody myRigid;
    private float Distance;

    public GameObject EnemyArt;
    public Animator Anim;
    public Transform[] FirePositions;
    private Canvas health;

    float MyArea = 5;
    float MyBulletsArea = 10;
    float AVBullet_Force = 100;
    float Sep_Force =2;
    float Coh_Force = 1;
    float AlignForce = 1;
    float EllapsedTime = 0;
    bool isFiring = false;
	bool isAlive = true;
    void OnEnable()
    {
        int CurrentLevel = Enemies_Manager.Instance.GetCurrentLevel();
        AttackDamage = DefaultAttackDamage * CurrentLevel;
        HP = DefaultHP * CurrentLevel;
        health = gameObject.GetComponentInChildren<Canvas>() ? gameObject.GetComponentInChildren<Canvas>() : null;
        healthBar.maxValue = HP;
        healthBar.value = HP;
        health.enabled = true;
        moveSpeed = DefaultMoveSpeed * CurrentLevel;
		isAlive = true;

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
        if (healthBar)
        {
            healthBar.value = HP;
        }
		if (isAlive) {
			Attack();
		} else {
			myRigid.velocity = Vector3.zero;
		}
    }
    
    public virtual void Patrol()
    {

    }
    public virtual void Attack()
    {
        if (Type == EnemyType.Melee || Distance > Range)
        {
            myRigid.velocity = Truncate(myRigid.velocity + ToPlayer() + Separation() + Alignment() + Cohesion() + AvoidBullets(), moveSpeed);
            if(isFiring)
                Invoke("StopFireBullets", 1.5f);
        }
        else
        {
            myRigid.velocity = Vector3.zero;
            if (!isFiring)
            {
                StartCoroutine("FireBullets");
            }
        }
        transform.LookAt(new Vector3(Target.position.x,transform.position.y, Target.position.z));
    }
    void StopFireBullets()
    {
        StopCoroutine("FireBullets");
        isFiring = false;
    }
    internal void ChangeSpeed(float percentage)
    {
        if (percentage!=-1)
        {
            moveSpeed *= percentage;
        }
        else
        {
            moveSpeed = DefaultMoveSpeed;
        }
            
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
    Vector3 AvoidBullets()
    {
        if (AvBullets)
        {
            Vector3 SteeringForce = Vector3.zero;
            Vector3 PushForce = Vector3.zero;
            foreach (GameObject bullet in Player_Controller.Instance.GetBasicWeapon().bulletPool.pooledObjects)
            {
                if (bullet.activeSelf == true)
                {
                    if (Vector3.Distance(myRigid.position, bullet.GetComponent<Rigidbody>().position) < MyBulletsArea)
                    {
                        //SetUp Push Force
                        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
                        // find right vector:

                        PushForce = (myRigid.position - bullet.GetComponent<Rigidbody>().position);
                        Vector3 right = Vector3.Cross(PushForce.normalized, up.normalized);
                        SteeringForce += right * Vector3.Distance(myRigid.position, bullet.GetComponent<Rigidbody>().position) / MyBulletsArea;

                    }
                }
            }
            SteeringForce = SteeringForce.normalized * AVBullet_Force;
            return SteeringForce;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public virtual void TakeDamage(int damage)
    {

        HP -= damage;
        if (HP <= 0)
        {          
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
		isAlive = false;
        StartCoroutine("death");
        gameObject.GetComponent<BoxCollider>().enabled = false;
        EnemyArt.SetActive( false);
        if (health)
        {
            health.enabled = false;
        }
        if (Explosion)
        {
            Explosion.Play();
        }
        if(!isCollidedWithEnemy)
        {
            DataHandler.Instance.AddEnemyKilled();
            GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_loose_cannon, 50, 1);
            GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_technomurderer, 75, 1);
            GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_mayhem, 100, 1);
            GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_robot_genocide, 150, 1);
            GameManager.Instance.UnlockAchievement(SurvivalCubeResources.achievement_robot_apocalypse, 500, 1);
        }
        //raise event
        if (OnEnemyDie != null) { OnEnemyDie(this.gameObject, AddedScore, AddedCoins, isCollidedWithEnemy); }
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            HP = 0;
            col.gameObject.GetComponent<Player_Controller>().TakeDamage(AttackDamage);
            Die(true);
        }

    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        EnemyArt.SetActive( true);
        Explosion.Stop();
        gameObject.SetActive(false);
    }
    IEnumerator FireBullets()
    {
        isFiring = true;
        while (true)
        {
            Anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.11f);
            foreach (Transform FirePos in FirePositions)
            {
                GameObject obj = Enemies_Manager.Instance.BulletPool.GetObject();
                if (obj)
                {
                    obj.transform.position = FirePos.position;
                    obj.transform.forward = FirePos.forward;
                    obj.transform.rotation = FirePos.rotation;
                    obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    obj.SetActive(true);
                    obj.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
