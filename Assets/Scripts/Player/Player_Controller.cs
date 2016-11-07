﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance;

    public LayerMask Plane;

    public Transform WeaponPos;
    public GameObject[] Weapons;
    public GameObject WaveClear;
    DefaultWeapon Weapon;
    GameObject BaiscWeapon;
    bool Upgraded = false;

    public int HitPoints = 100;
    public float DamageMultiplier = 1;

    public AnimationCurve motionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float Defaultspeed = 2f;
    public float DefaultFirRate = 0.05f;
    float Speed;
    float FirRate;
    [HideInInspector]
    public bool Buffed = false;
    Rigidbody _MyRig;
    int Dir = 0;
    float EllapsedTime = 0;

    public Slider healthBar;
    public ParticleSystem Explosion;
    float Ellapsed_Time = 0;
    bool ReversedControls = false;


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
        Explosion.Stop();
        gameObject.SetActive(false);
        GameManager.Instance.ThePlayerDied();
    }
    public void Move(float _Dir)
    {
        if (ReversedControls)
        {
            _Dir *= -1;
        }
        if (_Dir > 0.1f)
        {
            Ellapsed_Time += Time.deltaTime;
            float curvedValue = motionCurve.Evaluate(Ellapsed_Time);
            _MyRig.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * Speed * curvedValue);
        }
        else if (_Dir < -0.1)
        {
            Ellapsed_Time += Time.deltaTime;
            float curvedValue = motionCurve.Evaluate(Ellapsed_Time);
            _MyRig.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * Speed * curvedValue);
        }
        else
        {
            Ellapsed_Time = 0;
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
            if (GameManager.Instance.InGameCoins >= Weapons[index].GetComponent<DefaultWeapon>().Cost)
            {
                GameManager.Instance.InGameCoins -= Weapons[index].GetComponent<DefaultWeapon>().Cost;
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
    }
    public void ActivateWaveClear()
    {
        if (GameManager.Instance.InGameCoins >= WaveClear.GetComponent<WaveClear>().cost)
        {
            GameManager.Instance.InGameCoins -= WaveClear.GetComponent<WaveClear>().cost;

            GameObject.Instantiate(WaveClear, transform.position, WaveClear.transform.rotation);
        }
    }
    public void UpgradeBuffs(UpgradeBuffs _Data)
    {
        if (_Data._Speed)
        {
            Speed = _Data.Speed;
            if (popupBuffs)
            {
                GameObject popupText = GameObject.Instantiate(popupBuffs);
                popupText.transform.parent = this.transform;
                //popupText.transform.position = info.transform.position;

                popupText.GetComponent<PopUpBuffs>().setup("Speed", popupDouration, raiseSpeed);
            }

        }
        if (_Data._FireRate)
        {
            FirRate = _Data.FireRate;
            if (popupBuffs)
            {
                GameObject popupText = GameObject.Instantiate(popupBuffs);
                popupText.transform.parent = this.transform;
                //popupText.transform.position = info.transform.position;

                popupText.GetComponent<PopUpBuffs>().setup("FireRate", popupDouration, raiseSpeed);
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

                popupText.GetComponent<PopUpBuffs>().setup("Damage", popupDouration, raiseSpeed);
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
    }
    public void ReverseControls()
    {
        ReversedControls = true;
        StartCoroutine("NormalControls");
    }
    IEnumerator NormalControls()
    {
        yield return new WaitForSeconds(5);
        ReversedControls = false;

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

        Speed = Defaultspeed;
        FirRate = DefaultFirRate;
        DamageMultiplier = 1;
        Buffed = false;
    }
    void OnDisable()
    {
        InputManager.movementChanged -= Move;
        InputManager.attack -= Fire;
    }
}
