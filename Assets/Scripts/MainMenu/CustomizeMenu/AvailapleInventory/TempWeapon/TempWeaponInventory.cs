using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TempWeaponInventory : InventoryUI
{
    protected new void Awake()
    {
        base.Awake();
        //StartCoroutine(LoadWeapons());
    }
    IEnumerator LoadWeapons()
    {
        while (DataHandler.Instance.DataLoaded == false)
        {
            yield return null;
        }
        List<int> x = new List<int>(DataHandler.Instance.GetUnlockedTempWeaponsIDs());
        foreach (var ID in x)
        {
            GameObject G = Instantiate(ItemSlot);
            G.transform.parent = transform;
            G.GetComponent<ItemSlot>().ItemIndex = ID;
            G.GetComponent<ItemSlot>().ItemImage.sprite = GameManager.Instance.TempWeapons[ID].GetComponent<TempWeapon>().UISprite;
        }
    }
    public override void ReloadData()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine(LoadWeapons());
    }
}
