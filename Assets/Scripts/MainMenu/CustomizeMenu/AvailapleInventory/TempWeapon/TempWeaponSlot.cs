using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TempWeaponSlot : ItemSlot
{
    public new void OnClick()
    {
        //MainWeaponInfoOverlap.Instance.TempWeapon_ShowUse(ItemIndex, delegate ()
        // {
        //     DataHandler.Instance.SetTempWeaponToSlot(0,this.ItemIndex);
        // });
        MainMenuSliders.Instance.TempWeaponSelectedToSlot = this.ItemIndex;
    }
}