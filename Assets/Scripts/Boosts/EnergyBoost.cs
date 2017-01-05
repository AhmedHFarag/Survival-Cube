using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoost : Boost {
    
    public int Value;
    bool unlocked;
    
	// Use this for initialization
	void Start () {
		
	}
    public override void Add( int count)
    {
        DataHandler.Instance.AddEnergyBoost(ID, count);
    }
    public override void Toggle()
    {
        DataHandler.Instance.ToggleEnergyBoost(ID, is_Active);
        
    }
    public override void Activate()
    {
        base.Activate();
        DataHandler.Instance.ToggleEnergyBoost(ID, is_Active);

    }
    public override void Deactivate()
    {
        base.Deactivate();
        DataHandler.Instance.ToggleEnergyBoost(ID, is_Active);

    }
    public override bool CheckIfAllowed()
    {
        if (DataHandler.Instance.GetEnergyBoostAllowed(ID) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
	// Update is called once per frame
    
	void Update () {
		
	}
}
