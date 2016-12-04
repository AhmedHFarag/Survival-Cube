using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class multiplyTurret : TempWeapon
{

    void Start()
    {
        GameObject _obj = Instantiate(GameManager.Instance.GetMainWeapon());
        _obj.transform.position = transform.position;
        _obj.transform.parent = this.transform;
        _obj.transform.LookAt(transform.position - transform.forward);

        _obj = Instantiate(GameManager.Instance.GetMainWeapon());
        _obj.transform.position = transform.position;
        _obj.transform.parent = this.transform;
        _obj.transform.LookAt(transform.position - transform.right);

        _obj = Instantiate(GameManager.Instance.GetMainWeapon());
        _obj.transform.position = transform.position;
        _obj.transform.parent = this.transform;
        _obj.transform.LookAt(transform.position+transform.right);
    }

}
