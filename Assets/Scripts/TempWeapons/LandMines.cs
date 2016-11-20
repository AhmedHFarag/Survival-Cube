using UnityEngine;
using System.Collections;

public class LandMines : TempWeapon
{
    public GameObject Mine;
    public int MinesCount;
    int i = 0;
    float Ellapsedtime = 0;
    
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ellapsedtime += Time.deltaTime;

        if (i < MinesCount && Ellapsedtime >= 1)
        {
            i++;
            Ellapsedtime = 0;
            GameObject _obj = Instantiate(Mine);
            _obj.transform.position = transform.position;
            _obj.GetComponent<Rigidbody>().AddForce((transform.up + transform.forward)*300);
        }
    }

}
