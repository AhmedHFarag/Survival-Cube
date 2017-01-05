using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : Boost {

    public float Value;
    bool unlocked;

    // Use this for initialization
    void Start()
    {

    }
    public override void Add(int count)
    {
        DataHandler.Instance.AddDamageBoost(ID, count);
    }
    public override void Deactivate()
    {
        base.Deactivate();
        DataHandler.Instance.ToggleDamageBoost(ID, is_Active);

    }
    public override void Activate()
    {
        base.Activate();
        DataHandler.Instance.ToggleDamageBoost(ID, is_Active);

    }
    public override void Toggle()
    {
        DataHandler.Instance.ToggleDamageBoost(ID, is_Active);
    }
    public override bool CheckIfAllowed()
    {
        if (DataHandler.Instance.GetDamageBoostAllowed(ID) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
