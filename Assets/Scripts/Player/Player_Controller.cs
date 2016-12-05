using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance;

    public LayerMask Plane;

    public Transform WeaponPos;
    [HideInInspector]
    DefaultWeapon BasicWeapon;
    GameObject m_BaiscWeapon;
    [HideInInspector]
    public bool TempInUse = false;

    public int HitPoints = 100;
    public float DamageMultiplier = 1;

    public AnimationCurve motionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float Defaultspeed = 2f;
    public float AutoAimThreshold = 5f;
    float Speed;
    [HideInInspector]
    public bool Buffed = false;
    Rigidbody _MyRig;
    int Dir = 0;

    public Slider healthBar;
    public ParticleSystem Explosion;
    float Ellapsed_Time = 0;
    bool ReversedControls = false;
    //bool IsMoving = false;

    //popupBuffs
    public GameObject popupBuffs;

    public float popupDouration = 2;
    public float raiseSpeed = 2;


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
        _MyRig = GetComponent<Rigidbody>();
        InputManager.movementChanged += Move;
        InputManager.attack += Fire;
        m_BaiscWeapon = Instantiate(GameManager.Instance.GetMainWeapon());
        m_BaiscWeapon.transform.rotation = transform.rotation;
        m_BaiscWeapon.transform.position = WeaponPos.position;
        m_BaiscWeapon.transform.parent = transform;
        BasicWeapon = m_BaiscWeapon.GetComponent<DefaultWeapon>();
    }
    void Update()
    {
        if (InputManager.Instance.ControlSchemeTouch && Input.GetMouseButton(0))
        {
            //Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 200, Plane))
            {
                transform.LookAt(Hit.point+new Vector3(0,transform.position.y,0));
            }

        }
        
        ////Auto Aim 
        if (Enemies_Manager.Instance.activeEnemies.Count > 0)
        {
            //if (!IsMoving)
            {
                foreach (GameObject enemy in Enemies_Manager.Instance.activeEnemies)
                {
                    if (Mathf.Abs(Vector3.Angle(transform.forward, enemy.transform.position)) < AutoAimThreshold)
                    {
                        transform.LookAt(enemy.transform.position + new Vector3(0, transform.position.y, 0));
                    }
                }
            }
        }
        //IsMoving = false;
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
        Explosion.Stop();
        gameObject.SetActive(false);
        GameManager.Instance.ThePlayerDied();
    }
    public void Move(float _Xdir, float _Ydir)
    {
        //IsMoving = true;
        if (ReversedControls)
        {
            _Xdir *= -1;
        }
        if (InputManager.Instance.ControlSchemeJoyStick==true)
        {
            float heading = Mathf.Atan2(_Xdir, _Ydir);
            _MyRig.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg+45, 0f);
        }
        else
        {
            if (_Xdir > 0.1f)
            {
                //IsMoving = true;
                Ellapsed_Time += Time.deltaTime;
                float curvedValue = motionCurve.Evaluate(Ellapsed_Time);
                _MyRig.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * Speed * curvedValue);
            }
            else if (_Xdir < -0.1)
            {
                //IsMoving = true;
                Ellapsed_Time += Time.deltaTime;
                float curvedValue = motionCurve.Evaluate(Ellapsed_Time);
                _MyRig.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * Speed * curvedValue);
            }
            else
            {
                Ellapsed_Time = 0;
            }
        }
    }
    void Fire(bool attacking)
    {
        if (attacking)
        {
            BasicWeapon.Fire();
        }
    }
    public bool ActiveTempWeapon(int index)//Button Index
    {
        if (!TempInUse)
        {
            TempWeapon weap = GameManager.Instance.TempWeapons[DataHandler.Instance.GetTempWeapon(index)].GetComponent<TempWeapon>();
            if (DataHandler.Instance.GetInGameCoins() >= weap.Cost)
            {
                DataHandler.Instance.AddInGameCoins( -weap.Cost);
                //m_BaiscWeapon.SetActive(false);
                GameObject obj = Instantiate(weap.gameObject);
                obj.GetComponent<TempWeapon>().SelfInitialize(gameObject);
                TempInUse = true;
                return true;
            }
        }
        return false;
    }
    public void UpgradeBuffs(UpgradeBuffs _Data)
    {
        if (_Data._Speed)
        {
            Speed *= _Data.Speed;
            if (popupBuffs)
            {
                GameObject popupText = GameObject.Instantiate(popupBuffs);
                popupText.transform.parent = this.transform;
                //popupText.transform.position = info.transform.position;
                if (_Data.Speed > 1)
                {
                    popupText.GetComponent<PopUpBuffs>().setup("Speed++", popupDouration, raiseSpeed);
                }
                else
                {
                    popupText.GetComponent<PopUpBuffs>().setup("Speed--", popupDouration, raiseSpeed);
                }
            }

        }
        if (_Data._FireRate)
        {
            BasicWeapon.FireRate *= _Data.FireRate;
            if (popupBuffs)
            {
                GameObject popupText = GameObject.Instantiate(popupBuffs);
                popupText.transform.parent = this.transform;
                //popupText.transform.position = info.transform.position;
                if (_Data.FireRate > 1)
                {
                    popupText.GetComponent<PopUpBuffs>().setup("FireRate++", popupDouration, raiseSpeed);
                }
                else
                {
                    popupText.GetComponent<PopUpBuffs>().setup("FireRate--", popupDouration, raiseSpeed);
                }
            }
        }
        if (_Data._Damage)
        {
            DamageMultiplier = _Data.DamageMultiplier;
            if (popupBuffs)
            {
                GameObject popupText = GameObject.Instantiate(popupBuffs);
                popupText.transform.parent = this.transform;
                //popupText.transform.position = info.transform.position;
                if (_Data.DamageMultiplier > 1)
                {
                    popupText.GetComponent<PopUpBuffs>().setup("Damage++", popupDouration, raiseSpeed);
                }
                else
                {
                    popupText.GetComponent<PopUpBuffs>().setup("Damage--", popupDouration, raiseSpeed);
                }

            }
        }
        Buffed = true;
        StartCoroutine("DeBuff");
    }
    public void Heal(int healAmount)
    {
        HitPoints += healAmount;
        if (HitPoints > 100)
        {
            HitPoints = 100;
        }
        if (healthBar)
        {
            healthBar.value = HitPoints;
        }
        if (popupBuffs)
        {
            GameObject popupText = GameObject.Instantiate(popupBuffs);
            popupText.transform.parent = this.transform;
            //popupText.transform.position = info.transform.position;
            popupText.GetComponent<PopUpBuffs>().setup("Health +" + healAmount.ToString(), popupDouration, raiseSpeed);
        }
    }
    public void ReverseControls()
    {
        if (popupBuffs)
        {
            GameObject popupText = GameObject.Instantiate(popupBuffs);
            popupText.transform.parent = this.transform;
            //popupText.transform.position = info.transform.position;

            popupText.GetComponent<PopUpBuffs>().setup("Control Reversed", popupDouration, raiseSpeed);
        }
        ReversedControls = true;
        StartCoroutine("NormalControls");
    }
    public DefaultWeapon GetBasicWeapon()
    {
        return BasicWeapon;
    }
    IEnumerator NormalControls()
    {
        yield return new WaitForSeconds(5);
        ReversedControls = false;

    }
    IEnumerator DeBuff()
    {
        yield return new WaitForSeconds(5);

        Speed = Defaultspeed;
        BasicWeapon.FireRate = BasicWeapon._DefaultFireRate;
        DamageMultiplier = 1;
        Buffed = false;
    }
    void OnDisable()
    {
        InputManager.movementChanged -= Move;
        InputManager.attack -= Fire;
    }
}
