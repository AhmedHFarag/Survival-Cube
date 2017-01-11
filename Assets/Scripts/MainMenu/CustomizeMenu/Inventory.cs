using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Inventory : MonoBehaviour
{
    public GameObject ItemSlot;
    public abstract void ReloadData();
    protected void Awake()
    {
        DataHandler.DataChanged += ReloadData;
        //ReloadData();
    }
    
    private void OnDestroy()
    {
        DataHandler.DataChanged -= ReloadData;
    }
}
