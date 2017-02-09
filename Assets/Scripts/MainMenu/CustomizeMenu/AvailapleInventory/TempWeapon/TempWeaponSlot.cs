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
    public void Upgrade()
    {
        if (DataHandler.Instance.GetTempWeaponLevel(ItemIndex) < 2)
        {
            MainWeaponInfoOverlap.Instance.TempWeapon_ShowUpgrade(ItemIndex, delegate ()
            {
                GameManager.Instance.UpgradeTempWeapon(this.ItemIndex);
            });
        }
    }
}