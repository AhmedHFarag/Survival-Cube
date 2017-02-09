using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeaponInGameSlot : ItemSlot
{
    public int SlotNumber =0;
    public new void OnClick()
    {
        
        DataHandler.Instance.SetTempWeaponToSlot(SlotNumber, MainMenuSliders.Instance.TempWeaponSelectedToSlot);
        MainMenuSliders.Instance.TempWeaponSelectedToSlot = -1;
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
    private void FixedUpdate()
    {
        if (MainMenuSliders.Instance.TempWeaponSelectedToSlot>-1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.PingPong(Time.time*50, 10)));
        }
    }
}
