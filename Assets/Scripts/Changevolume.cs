using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changevolume : MonoBehaviour {
    private AudioSource bgs;
    public Slider s;
    // Use this for initialization
    void Start () {
   
      
        bgs = gameObject.GetComponent<AudioSource>();
        s.value = DataHandler.Instance.GetBgVolume();
        bgs.volume = DataHandler.Instance.GetBgVolume();
    }
    public void ChangeVolume()
    {
        
        bgs.volume = s.value;
        DataHandler.Instance.SetBgVolume(bgs.volume);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
