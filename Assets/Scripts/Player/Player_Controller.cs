using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance;
    public DefaultWeapon Weapon;
    public LayerMask Plane;
    GameObject BaiscWeapon;
    bool Upgraded = false;
    public Transform WeaponPos;
    public GameObject[] Weapons;
    public int HitPoints = 100;
    public int Damage = 10;
    public float speed = 2f;
    Rigidbody _MyRig;
    int Dir = 0;
    float EllapsedTime = 0;

    public Slider healthBar;

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
    void Start()
    {
        _MyRig = GetComponent<Rigidbody>();
        InputManager.movementChanged += Move;
        InputManager.attack += Fire;
        BaiscWeapon = Instantiate(Weapons[0]);
        BaiscWeapon.transform.rotation = transform.rotation;
        BaiscWeapon.transform.position = WeaponPos.position;
        BaiscWeapon.transform.parent = transform;
        Weapon = BaiscWeapon.GetComponent<DefaultWeapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EllapsedTime += Time.deltaTime;

    }
    void Update()
    {
        if (InputManager.Instance.ControlScheme2)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 200, Plane))
            {
                transform.LookAt(Hit.point);
            }
            
        }
        if (EllapsedTime > 0.05f)
        {
            EllapsedTime = 0;
            Weapon.Fire();
        }

    }
    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        if (healthBar)
        {
            healthBar.value = HitPoints;
        }
        if (HitPoints <= 0)
            Die();
    }
    public void Die()
    {
        //gameManager event where the player dies
        GameManager.Instance.ThePlayerDied();
        //gameObject.SetActive(false);
    }
    public void Move(float _Dir)
    {
        
        if (_Dir > 0.1f)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * speed);
            if (EllapsedTime > 0.1f)
            {
                EllapsedTime = 0;
#if UNITY_EDITOR

                Weapon.Fire();
#else
                //if(InputManager.Instance.ControlScheme0)
                //{
                //    Weapon.Fire();
                //}
#endif
            }
        }
        else if (_Dir < -0.1)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * speed);
            if (EllapsedTime > 0.1f)
            {
                EllapsedTime = 0;
#if UNITY_EDITOR
                Weapon.Fire();
#else
                //if(InputManager.Instance.ControlScheme0)
                //{
                //    Weapon.Fire();
                //}
                
#endif
            }
        }
    }
    void Fire(bool attacking)
    {
        if (attacking)
        {
            Weapon.Fire();
        }
    }
    public void UpgradeWeapon()
    {
        if (!Upgraded)
        {
            Weapon = null;
            BaiscWeapon.SetActive(false);
            StartCoroutine("NewWeapon");
            GameObject obj = Instantiate(Weapons[1]);
            obj.transform.rotation = transform.rotation;
            obj.transform.position = WeaponPos.position;
            obj.transform.parent = transform;
            Weapon = obj.GetComponent<DefaultWeapon>();
            Upgraded = true;

        }
    }
    IEnumerator NewWeapon()
    {
        yield return new WaitForSeconds(5);
        Destroy(Weapon.gameObject);
        BaiscWeapon.SetActive(true);
        Weapon = BaiscWeapon.GetComponent<DefaultWeapon>();
        Upgraded = false;
    }
    void OnDisable()
    {
        InputManager.movementChanged -= Move;
        InputManager.attack -= Fire;
    }
}
