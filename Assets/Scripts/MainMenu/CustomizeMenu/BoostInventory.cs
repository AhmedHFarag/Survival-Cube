using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostInventory : Inventory {

    override public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(" - ");
        int index = 0;
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                int x = item.GetComponent<DragHandeler>().WeaponID;
                DataHandler.Instance.SetTempWeapon(index, x);
                builder.Append(item.name);
                builder.Append(" - ");
            }
            index++;
        }
        if (inventoryText)
            inventoryText.text = builder.ToString();
    }
    
}
