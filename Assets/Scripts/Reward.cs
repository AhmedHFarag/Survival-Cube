using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {
    public int Coins;
    public int EnergyBoost;
    bool Unlocked;
    public int Day;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void ActivateReward()
    {
        if(Coins>0)
        {
            DataHandler.Instance.AddCoins(Coins);
        }
        if(EnergyBoost>0)
        {
            DataHandler.Instance.SetPlayerStartingEnergy(EnergyBoost);
        }
    }
}
