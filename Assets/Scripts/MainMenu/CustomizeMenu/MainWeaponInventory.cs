using UnityEngine;
using System.Collections;

public class MainWeaponInventory : Inventory
{

    override public void HasChanged()
    {
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                int x = item.GetComponent<DragHandeler>().WeaponID;
                DataHandler.Instance.SteMainWeaponID(x);
            }
        }
    }
}
