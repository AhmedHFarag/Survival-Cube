using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changevolume : MonoBehaviour {
   
    public Slider s;
    // Use this for initialization
    void Start () {
        s.value = DataHandler.Instance.GetBgVolume();
        AudioListener.volume= DataHandler.Instance.GetBgVolume();
    }
    public void ChangeVolume()
    {
        AudioListener.volume = s.value;
        DataHandler.Instance.SetBgVolume(AudioListener.volume);
    }
  
}
