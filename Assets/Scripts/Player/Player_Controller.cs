using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
    public static Player_Controller Instance;
    public DefaultWeapon Weapon;
    public Transform WeaponPos;
    public GameObject[] Weapons;
    public int HitPoints=100;
    public int Damage=10;
    public float speed=2f;
    Rigidbody _MyRig;
    int Dir = 0;
    float EllapsedTime = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        
    }
	void Start ()
    {
        _MyRig = GetComponent<Rigidbody>();
        InputManager.movementChanged += Move;
        GameObject obj=Instantiate(Weapons[0]);

        obj.transform.position = WeaponPos.position;
        obj.transform.parent = transform;
        Weapon = obj.GetComponent<DefaultWeapon>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        EllapsedTime += Time.deltaTime;

    }
    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
            Die();
    }
    public void Die()
    {
        //gameManager event where the player dies
        gameObject.SetActive(false);
    }
    public void Move(float _Dir)
    {
       if(_Dir>0.1f)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * speed);
            if (EllapsedTime > 0.1f)
            {
                EllapsedTime = 0;
                Weapon.Fire();
            }
        }
        else if (_Dir < -0.1)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * speed);
            if (EllapsedTime > 0.1f)
            {
                EllapsedTime = 0;
                Weapon.Fire();
            }
        }
    }
}
