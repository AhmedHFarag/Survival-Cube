using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BundleSlot : MonoBehaviour
{
    public int Value;
    public float Price;
    public Text BundleText;

    public void OnClick()
    {
        if (PlayerPrefs.HasKey(Value+"CoinsPrice"))
        {
            //request to buy the value of coins with the stored price
        }
    }
}
	
