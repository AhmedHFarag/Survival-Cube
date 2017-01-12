using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedWeaponInventory : Inventory {

    protected new void Awake()
    {
        base.Awake();
        //StartCoroutine(LoadWeapons());
    }
    IEnumerator LoadEquipedMainWeapon()
    {
        
        while (DataHandler.Instance.DataLoaded == false)
        {
            yield return null;
        }
            GameObject G = Instantiate(ItemSlot);
            G.transform.parent = transform;
            G.GetComponent<ItemSlot>().ItemIndex = DataHandler.Instance.GetMainWeaponID();
        G.GetComponent<ItemSlot>().ItemImage.sprite = GameManager.Instance.Weapons[DataHandler.Instance.GetMainWeaponID()].GetComponent<DefaultWeapon>().UISprite;
    }
    public override void ReloadData()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine(LoadEquipedMainWeapon());
    }
}
