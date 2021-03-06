﻿using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{
    public int Damage;
    public float Speed = 2;
    public Rigidbody MyRigid;
    //private TrailRenderer tr;
    public AudioClip sound;
    protected float soundVolume = 1.0f;
    [SerializeField]
    public float timeForSelfDestory = 1;

    // Use this for initialization
    void OnEnable()
    {
        //tr = GetComponent<TrailRenderer>();
        //tr.Clear();
        StartCoroutine(SelfDestory());
    }
    void Start()
    {
        
        if (MyRigid == null)
        {
            MyRigid = gameObject.GetComponent<Rigidbody>();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("Bullet"))
        {
            if (other.collider.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage((int)Mathf.Floor(Damage * Player_Controller.Instance.DamageMultiplier));
                StopCoroutine("SelfDestory");
                gameObject.SetActive(false);

            }
        }
        else
        {
            if(other.collider.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player_Controller>().TakeDamage(Damage);
                StopCoroutine("SelfDestory");
                gameObject.SetActive(false);

            }
        }
    }

    protected IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(timeForSelfDestory);
        MyRigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
