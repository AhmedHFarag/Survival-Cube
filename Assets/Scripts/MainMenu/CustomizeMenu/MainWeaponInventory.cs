using UnityEngine;
using System.Collections;

public class MainWeaponInventory : Inventory
{

    override public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                int x = item.GetComponent<DragHandeler>().WeaponID;
                DataHandler.Instance.SteMainWeaponID(x);
                builder.Append(item.name);
                builder.Append(" - ");
                
            }
        }
        if (inventoryText)
            inventoryText.text = builder.ToString();
    }
}
