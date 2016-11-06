using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance;

    public LayerMask Plane;
    
    public Transform WeaponPos;
    public GameObject[] Weapons;
    DefaultWeapon Weapon;
    GameObject BaiscWeapon;
    bool Upgraded = false;

    public int HitPoints = 100;
    public int Damage = 10;

    public float Defaultspeed = 2f;
    public float DefaultFirRate = 0.05f;
    float Speed;
    float FirRate;
    [HideInInspector]
    public bool Buffed=false;
    Rigidbody _MyRig;
    int Dir = 0;
    float EllapsedTime = 0;

    public Slider healthBar;
    public ParticleSystem Explosion;
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
        Speed = Defaultspeed;
        FirRate = DefaultFirRate;
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
        if (EllapsedTime > FirRate)
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
        StartCoroutine("death");
        foreach (var item in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            item.enabled = false;
        }
        Explosion.Play();
    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(1f);
        //gameObject.GetComponent<BoxCollider>().enabled = true;
        //gameObject.GetComponent<MeshRenderer>().enabled = true;
        Explosion.Stop();
        gameObject.SetActive(false);
        GameManager.Instance.ThePlayerDied();

    }
    public void Move(float _Dir)
    {
        
        if (_Dir > 0.1f)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * Speed);
        }
        else if (_Dir < -0.1)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * Speed);
        }
    }
    void Fire(bool attacking)
    {
        if (attacking)
        {
            Weapon.Fire();
        }
    }
    public void UpgradeWeapon(int index)
    {
        if (!Upgraded)
        {
            Weapon = null;
            BaiscWeapon.SetActive(false);
            StartCoroutine("NewWeapon");
            GameObject obj = Instantiate(Weapons[index]);
            obj.transform.rotation = transform.rotation;
            obj.transform.position = WeaponPos.position;
            obj.transform.parent = transform;
            Weapon = obj.GetComponent<DefaultWeapon>();
            Upgraded = true;

        }
    }
    public void UpgradeBuffs(UpgradeBuffs _Data)
    {
        if (_Data._Speed)
        {
            Debug.Log("Speed");
            Speed = _Data.Speed;
        }
        if (_Data._FireRate)
        {
            Debug.Log("FireRate");
            FirRate = _Data.FireRate;
        }
        Buffed = true;
        StartCoroutine("DeBuff");
    }
    public void DeBuffs()
    {
        Debug.Log("Debuff");
        Speed = Defaultspeed;
        FirRate = DefaultFirRate;
        Buffed = false;
    }
    IEnumerator NewWeapon()
    {
        yield return new WaitForSeconds(5);
        Destroy(Weapon.gameObject);
        BaiscWeapon.SetActive(true);
        Weapon = BaiscWeapon.GetComponent<DefaultWeapon>();
        Upgraded = false;
    }
    IEnumerator DeBuff()
    {
        yield return new WaitForSeconds(5);
        DeBuffs();
    }
    void OnDisable()
    {
        InputManager.movementChanged -= Move;
        InputManager.attack -= Fire;
    }
}
