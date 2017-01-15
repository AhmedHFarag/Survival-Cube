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
    private void FixedUpdate()
    {
        if (MainMenuSliders.Instance.TempWeaponSelectedToSlot>-1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.PingPong(Time.time*50, 10)));
        }
    }
}
