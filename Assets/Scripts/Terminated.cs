using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminated : MonoBehaviour {
     AudioSource ter;
	// Use this for initialization
	void Awake ()
    {
        ter = GetComponent<AudioSource>();
    }
	
	void OnEnable()
    {
        if (Player_Controller.Instance.HitPoints <= 0)
        {
                ter.Play();
        }      
        else
        {
            ter.Stop();
        }
    }
}
