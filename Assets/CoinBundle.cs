using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class CoinBundle : MonoBehaviour
{
    public string ID;
    public Text TextUI;
    public string Price;
    public string Title;
    bool isInitialized = false;
    // Use this for initialization
    void Start()
    {

    }

    public void OnClick()
    {
        IAPManager.Instance.OnPurchaseClicked(ID);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
