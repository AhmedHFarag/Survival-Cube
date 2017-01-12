using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedTempWeaponSlots : Inventory
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
        foreach (var item in DataHandler.Instance.Get_InGameTempweaponsSlots())
        {
            if (item.UnlockStatus)
            {
                GameObject G = Instantiate(ItemSlot);
                G.transform.parent = transform;
                G.GetComponent<ItemSlot>().ItemIndex = item.WeaponID;
                if (item.WeaponID>=0)
                {
                    G.GetComponent<ItemSlot>().ItemImage.sprite = GameManager.Instance.TempWeapons[item.WeaponID].GetComponent<TempWeapon>().UISprite;
                }                
            }
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
