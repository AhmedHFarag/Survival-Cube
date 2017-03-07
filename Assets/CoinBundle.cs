using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using OnePF;
using UnityEngine.UI;
public class CoinBundle : MonoBehaviour {
    public string ID;
    public Text TextUI;
    public string Price;
    public string Title;
    public BundlePurchase PurchaseManager;
	// Use this for initialization
	void Start () {
        Price = PurchaseManager._inventory.GetSkuDetails(ID).Price;
        Title = PurchaseManager._inventory.GetSkuDetails(ID).Title;

        TextUI.text = Title + " = " + Price;
	}
	public new void OnClick()
    {
        PurchaseManager.Purchase(ID);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
