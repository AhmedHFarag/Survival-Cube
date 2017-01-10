using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainWeaponInventory : Inventory
{
    protected new void Start()
    {
        base.Start();
        //StartCoroutine(LoadWeapons());
    }
    IEnumerator LoadWeapons()
    {
        while (DataHandler.Instance.DataLoaded == false)
        {
            yield return null;
        }
        List<int> x = new List<int>(DataHandler.Instance.GetUnlockedMainWeaponsID());
        foreach (var ID in x)
        {
            GameObject G = Instantiate(ItemSlot);
            G.transform.parent = transform;
            G.GetComponent<MainWeaponSlot>().ItemIndex = ID;
        }
    }
    public override void ReloadData()
    {
        StartCoroutine(LoadWeapons());
    }
}
