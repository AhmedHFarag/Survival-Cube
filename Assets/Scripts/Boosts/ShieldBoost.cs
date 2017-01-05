using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : Boost {

    public float Value;
    bool unlocked;

    // Use this for initialization
    void Start()
    {

    }
    public override void Add(int count)
    {
        DataHandler.Instance.AddShieldBoost(ID, count);
    }
    public override void Activate()
    {
        base.Activate();
        DataHandler.Instance.ToggleShieldBoost(ID, is_Active);

    }
    public override void Deactivate()
    {
        base.Deactivate();
        DataHandler.Instance.ToggleShieldBoost(ID, is_Active);

    }
    public override void Toggle()
    {
        DataHandler.Instance.ToggleShieldBoost(ID, is_Active);
    }
    public override bool CheckIfAllowed()
    {
        if (DataHandler.Instance.GetShieldBoostAllowed(ID) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
