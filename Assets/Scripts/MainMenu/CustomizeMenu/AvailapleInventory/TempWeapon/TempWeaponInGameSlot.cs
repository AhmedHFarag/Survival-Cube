using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeaponInGameSlot : ItemSlot
{
    public new void OnClick()
    {
        MainWeaponInfoOverlap.Instance.TempWeapon_ShowUse(ItemIndex, delegate ()
        {
            //DataHandler.Instance.SetMainWeaponID(this.ItemIndex);
        });
    }
}
