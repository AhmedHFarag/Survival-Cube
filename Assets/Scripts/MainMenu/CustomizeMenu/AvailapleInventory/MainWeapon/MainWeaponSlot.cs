using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainWeaponSlot : ItemSlot
{
    public new void OnClick()
    {
        MainWeaponInfoOverlap.Instance.ShowUse(ItemIndex,delegate() 
        {
            DataHandler.Instance.SetMainWeaponID(this.ItemIndex);
        });
    }
}