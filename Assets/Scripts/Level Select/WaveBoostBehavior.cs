using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveBoostBehavior : MonoBehaviour {
    public WaveData SelectedWave;
    public Text WaveNumberText;
	// Use this for initialization
	void Start () {
        WaveNumberText.text = "Wave " + SelectedWave.WaveNumber;
	}
	public void StartGame()
    {
        
        GameManager.Instance.Currentlevel=SelectedWave.LevelNumber;
        GameManager.Instance.CurrentWaveNumber=SelectedWave.WaveNumber-1;
        GameManager.Instance.StartGame();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
