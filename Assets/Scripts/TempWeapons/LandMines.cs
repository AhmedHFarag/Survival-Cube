using UnityEngine;
using System.Collections;

public class LandMines : TempWeapon
{
    public GameObject Mine;
    int MinesCount=4;
    int i = 0;
    float Ellapsedtime = 0;
    Vector3[] Directions=new Vector3[4];
    void Start()
    {
        Directions[0] = transform.forward+transform.up;
        Directions[1] = -transform.forward+transform.up;
        Directions[2] = transform.right+transform.up;
        Directions[3] = -transform.right + transform.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ellapsedtime += Time.deltaTime;

        if (i < MinesCount && Ellapsedtime >= 1)
        {
            Ellapsedtime = 0;
            GameObject _obj = Instantiate(Mine);
            _obj.transform.position = transform.position;
            _obj.GetComponent<Rigidbody>().AddForce(Directions[i] * 300);
            i++;
        }
    }

}
