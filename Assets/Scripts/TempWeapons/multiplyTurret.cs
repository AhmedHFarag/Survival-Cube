using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class multiplyTurret : TempWeapon
{

    DefaultWeapon BasicWeapon;
    public GameObject turret;
    int turretCount = 3;
    float Ellapsedtime = 0;
    int i = 0;
    private GameObject player;
    float[] Angles = new float[3];
    GameObject[] weapons = new GameObject[3];
    void Start()
    {
        BasicWeapon = turret.GetComponent<DefaultWeapon>();
        player = GameObject.FindGameObjectWithTag("Player");
        Angles[0] = Player_Controller.Instance.WeaponPos.eulerAngles.y+90;
        Angles[1] = Player_Controller.Instance.WeaponPos.eulerAngles.y - 90;
        Angles[2] = Player_Controller.Instance.WeaponPos.eulerAngles.y + 180;
    }
    void OnEnable()
    {
        StartCoroutine(RemoveTurrets());
    }
    void FixedUpdate()
    {
        Ellapsedtime += Time.deltaTime;

        if (i < turretCount && Ellapsedtime >= 1)
        {
            Ellapsedtime = 0;
            GameObject _obj = Instantiate(turret);
            _obj.transform.position = Player_Controller.Instance.WeaponPos.position ;
            _obj.transform.parent = player.transform;
            _obj.transform.Rotate(new Vector3(0, 1, 0), Angles[i]);
            weapons[i] = _obj;
            i++;
        }
    }
    IEnumerator RemoveTurrets()
    {
        yield return new WaitForSeconds(LifeTime);
        foreach (GameObject obj in weapons)
        {
            Destroy(obj);
        }
        Debug.Log("Slow time destroued");
    }
}
