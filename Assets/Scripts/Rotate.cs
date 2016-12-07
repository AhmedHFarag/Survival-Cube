using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    private Transform ts;
    private float rotationSpeed;
    // Use this for initialization
    void Start () {
	ts  = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
     
        ts.Rotate(new Vector3(0, 1, 0), 2f);

    

    }
}
