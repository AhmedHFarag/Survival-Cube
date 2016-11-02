using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
    public static Player_Controller Instance;
    public GameObject Bullet;
    public Transform FirePos;
    public int HitPoints=100;
    public int Damage=10;
    public float speed=2f;
    Rigidbody _MyRig;
    int Dir = 0;
    float EllapsedTime = 0;

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
	void Start ()
    {
        _MyRig = GetComponent<Rigidbody>();
        InputManager.movementChanged += Move;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        EllapsedTime += Time.deltaTime;

    }
    public void Move(float _Dir)
    {
       if(_Dir>0.1f)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.right), Time.deltaTime * speed);
            if (EllapsedTime > 0.1f)
            {
                EllapsedTime = 0;
                GameObject Shot = Instantiate(Bullet) as GameObject;
                Shot.transform.position = FirePos.position;
                Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                Destroy(Shot, 1);
            }
        }
        else if (_Dir < -0.1)
        {
            _MyRig.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.right), Time.deltaTime * speed);
            if (EllapsedTime > 0.1f)
            {
                EllapsedTime = 0;
                GameObject Shot = Instantiate(Bullet) as GameObject;
                Shot.transform.position = FirePos.position;
                Shot.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                Destroy(Shot, 1);
            }
        }
    }
}
