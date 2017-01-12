using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainWeaponSlot : ItemSlot
{
    public new void OnClick()
    {
        MainWeaponInfoOverlap.Instance.MainWeapon_ShowUse(ItemIndex,delegate() 
        {
            DataHandler.Instance.SetMainWeaponID(this.ItemIndex);
        });
    }
}