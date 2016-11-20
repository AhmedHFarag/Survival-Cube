using UnityEngine;
using System.Collections;

public class LandMines : TempWeapon {
    public GameObject Mine;
    public int MinesCount;
    
    // Use this for initialization
    void Start () {
        for (int i = 0; i < MinesCount; i++)
        {
            Instantiate(Mine).GetComponent<Rigidbody>().AddForce(transform.up+transform.forward);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
    
}
