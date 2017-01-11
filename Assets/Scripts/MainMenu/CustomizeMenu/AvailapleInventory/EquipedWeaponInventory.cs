using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedWeaponInventory : Inventory {

    protected new void Start()
    {
        base.Start();
        //StartCoroutine(LoadWeapons());
    }
    IEnumerator LoadEquipedMainWeapon()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
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
        StartCoroutine(LoadEquipedMainWeapon());
    }
}
