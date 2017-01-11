using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopTempWeaponSlot : ItemSlot
{
    public new void OnClick()
    {
        MainWeaponInfoOverlap.Instance.TempWeapon_ShowBuy(ItemIndex, delegate ()
         {
             //DataHandler.Instance.SetMainWeaponID(this.ItemIndex);
             Buy();
         });
    }
    public void Buy()
    {
        if (!DataHandler.Instance.GetTempWeaponSlotStatus(ItemIndex))// unlock Item if there is enogh coins
        {
            if (DataHandler.Instance.GetPlayerCoins() >= GameManager.Instance.TempWeapons[ItemIndex].GetComponent<TempWeapon>().UnlockCost)
            {


                MSGScript.Instance.Choice("Would you like to buy??", new UnityAction(() =>
                {
                    DataHandler.Instance.AddCoins(-GameManager.Instance.TempWeapons[ItemIndex].GetComponent<TempWeapon>().UnlockCost);
                        //Save unlocked Main weapon 
                        DataHandler.Instance.UnlockTempWeapon(ItemIndex);
                })
               , new UnityAction(() => { }), new UnityAction(() => { }));


            }
            else
            {
                MSGScript.Instance.OK("You Do not have enough Coins!!", () =>
                {

                });
            }
        }
    }
}