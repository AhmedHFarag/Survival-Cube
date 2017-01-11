﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMainWeaponsInventory : Inventory {
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
        List<int> x = new List<int>(DataHandler.Instance.GetLockedMainWeaponsID());
        foreach (var ID in x)
        {
            GameObject G = Instantiate(ItemSlot);
            G.transform.parent = transform;
            G.GetComponent<ItemSlot>().ItemIndex = ID;
            G.GetComponent<ItemSlot>().ItemImage.sprite = GameManager.Instance.Weapons[ID].GetComponent<DefaultWeapon>().UISprite;
        }
    }
    public override void ReloadData()
    {
        StartCoroutine(LoadWeapons());
    }
}
