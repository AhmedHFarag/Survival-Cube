using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BoostType
{
    Energy10 = 0,
    Energy25 = 1,
    Energy50 = 2,
    Energy100 = 3,
    FireRatex1_5 = 4,
    FireRatex2 = 5,
    FireRatex2_5 = 6,
    Damagex2 = 7,
    Shield10 = 8,
    Shield25 = 9,
    Shield50 = 10,
    Shield100 = 11
}
public class Boost : MonoBehaviour {
    public BoostType Type;
    public int ID;
    internal int is_Active=0;
    public int Cost;
    int count;
    // Use this for initialization
	void Start () {
		
	}
	public virtual void Add(int count)
    {
        switch (Type)
        {
            case BoostType.Energy10:
                DataHandler.Instance.AddBoost(0);
                break;
            case BoostType.Energy25:
                DataHandler.Instance.AddBoost(1);
                break;
            case BoostType.Energy50:
                DataHandler.Instance.AddBoost(2);
                break;
            case BoostType.Energy100:
                DataHandler.Instance.AddBoost(3); 
                break;
            case BoostType.FireRatex1_5:
                DataHandler.Instance.AddBoost(4);
                break;
            case BoostType.FireRatex2:
                DataHandler.Instance.AddBoost(5);
                break;
            case BoostType.FireRatex2_5:
                DataHandler.Instance.AddBoost(6);
                break;
            case BoostType.Damagex2:
                DataHandler.Instance.AddBoost(7);
                break;
            case BoostType.Shield10:
                DataHandler.Instance.AddBoost(8);
                break;
            case BoostType.Shield25:
                DataHandler.Instance.AddBoost(9);
                break;
            case BoostType.Shield50:
                DataHandler.Instance.AddBoost(10);
                break;
            case BoostType.Shield100:
                DataHandler.Instance.AddBoost(11);
                break;
            default:
                break;
        }
    }
	// Update is called once per frame
    public virtual void Activate()
    {
        is_Active = 1;
        
    }
    public virtual void Deactivate()
    {
        is_Active = 0;
    }
	public virtual void Toggle()
    {
        if(is_Active>0)
        {

        }
        #region old boost
        //switch (Type)
        //{
        //    case BoostType.Energy10:
        //        DataHandler.Instance.SetPlayerStartingEnergy(10);
        //        DataHandler.Instance.ActivateBoost(0);
        //        break;
        //    case BoostType.Energy25:
        //        DataHandler.Instance.ActivateBoost(1);
        //        DataHandler.Instance.SetPlayerStartingEnergy(25);
        //        break;
        //    case BoostType.Energy50:
        //        DataHandler.Instance.SetPlayerStartingEnergy(50);
        //        DataHandler.Instance.ActivateBoost(2);
        //        break;
        //    case BoostType.Energy100:
        //        DataHandler.Instance.SetPlayerStartingEnergy(100);
        //        DataHandler.Instance.ActivateBoost(3);
        //        break;
        //    case BoostType.FireRatex1_5:
        //        DataHandler.Instance.SetFireRateMultiplyer(1.5f);
        //        DataHandler.Instance.ActivateBoost(4);
        //        break;
        //    case BoostType.FireRatex2:
        //        DataHandler.Instance.SetFireRateMultiplyer(2f);
        //        DataHandler.Instance.ActivateBoost(5);
        //        break;
        //    case BoostType.FireRatex2_5:
        //        DataHandler.Instance.SetFireRateMultiplyer(2f);
        //        DataHandler.Instance.ActivateBoost(6);
        //        break;
        //    case BoostType.Damagex2:
        //        DataHandler.Instance.SetDamageMultiplyer(2f);
        //        DataHandler.Instance.ActivateBoost(7);
        //        break;
        //    case BoostType.Shield10:
        //        DataHandler.Instance.SetShield(10);
        //        DataHandler.Instance.ActivateBoost(8);
        //        break;
        //    case BoostType.Shield25:
        //        DataHandler.Instance.SetShield(25);
        //        DataHandler.Instance.ActivateBoost(9);
        //        break;
        //    case BoostType.Shield50:
        //        DataHandler.Instance.SetShield(50);
        //        DataHandler.Instance.ActivateBoost(10);
        //        break;
        //    case BoostType.Shield100:
        //        DataHandler.Instance.SetShield(100);
        //        DataHandler.Instance.ActivateBoost(11);
        //        break;
        //    default:
        //        break;
        // }
        #endregion
    }
    public virtual bool CheckIfAllowed()
    {
        return true;
    }
}
