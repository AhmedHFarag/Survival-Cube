using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EquipedMainWeaponSlot : ItemSlot
{
    public new void OnClick()
    {
        MainWeaponInfoOverlap.Instance.MainWeapon_ShowInfo(ItemIndex);
    }
    public void Upgrade()
    {
        if (DataHandler.Instance.GetMainWeaponLevel(ItemIndex) < 2)
        {
            MainWeaponInfoOverlap.Instance.MainWeapon_ShowUpgrade(ItemIndex, delegate ()
            {
                GameManager.Instance.UpgradeMainWeapon(this.ItemIndex);
            });
        }
    }
}