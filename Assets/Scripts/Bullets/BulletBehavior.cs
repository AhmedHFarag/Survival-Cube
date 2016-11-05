﻿using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{
    public int Damage;
    Rigidbody MyRigid;

    [SerializeField]
    private float timeForSelfDestory = 1;

    // Use this for initialization
    void OnEnable()
    {
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
        if (other.collider.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            Debug.Log("enemy");
        }
    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(timeForSelfDestory);
        MyRigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
