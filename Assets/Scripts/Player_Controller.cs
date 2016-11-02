using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

    public GameObject Bullet;
    public Transform FirePos;
    Rigidbody _MyRig;
    int Dir = 0;

	// Use this for initialization
	void Start ()
    {
        _MyRig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Dir > 0.1)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * 2.0f);
            GameObject Shot = Instantiate(Bullet) as GameObject;
            Shot.transform.position = FirePos.position;
            Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            Destroy(Shot, 1);
        }
        else if (Dir < -0.1)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * 2.0f);
            GameObject Shot = Instantiate(Bullet) as GameObject;
            Shot.transform.position = FirePos.position;
            Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            Destroy(Shot, 1);
        }
        else
        {

        }
    }
    public void Move(int _Dir)
    {
        Dir = _Dir;
    }
}
