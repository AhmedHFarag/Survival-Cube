using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoost : Boost {

    public float Value;
    bool unlocked;

    // Use this for initialization
    void Start()
    {

    }
    public override void Add(int count)
    {
        DataHandler.Instance.AddFireRateBoost(ID, count);
    }
    public override void Activate()
    {
        base.Activate();
        DataHandler.Instance.ToggleFireRateBoost(ID, is_Active);

    }
    public override void Deactivate()
    {
        base.Deactivate();
        DataHandler.Instance.ToggleFireRateBoost(ID, is_Active);

    }
    public override void Toggle()
    {
        DataHandler.Instance.ToggleFireRateBoost(ID, is_Active);
    }
    public override bool CheckIfAllowed()
    {
        if (DataHandler.Instance.GetFireRateBoostAllowed(ID) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void Update () {
		
	}
}
