using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopMainWeaponSlot : ItemSlot
{
    public new void OnClick()
    {
        MainWeaponInfoOverlap.Instance.MainWeapon_ShowBuy(ItemIndex, delegate ()
         {
             //DataHandler.Instance.SetMainWeaponID(this.ItemIndex);
             Buy();
        });
    }
    public void Buy()
    {
        if (!DataHandler.Instance.GetMainWeaponSlotStatus(ItemIndex))// unlock Item if there is enogh coins
        {
            if (DataHandler.Instance.GetPlayerCoins() >= GameManager.Instance.TempWeapons[ItemIndex].GetComponent<DefaultWeapon>().Cost)
            {


                MSGScript.Instance.Choice("Would you like to buy??", new UnityAction(() =>
                {
                    DataHandler.Instance.AddCoins(-GameManager.Instance.TempWeapons[ItemIndex].GetComponent<DefaultWeapon>().Cost);
                        //Save unlocked Main weapon 
                        DataHandler.Instance.UnlockMainWeapon(ItemIndex);
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