using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class WaveData : MonoBehaviour,IPointerClickHandler {
    public int WaveNumber;
    public int Unlocked;
    public int LevelNumber;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelSelectBehavior.Instance.WaveChoice(this);
    }
}
