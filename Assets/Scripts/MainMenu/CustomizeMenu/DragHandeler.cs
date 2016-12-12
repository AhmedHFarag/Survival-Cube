using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    public int WeaponID;
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    [HideInInspector]
    public Inventory MyInventory;
    public enum Weapontype
    {
        Main,Temp
    }
    public Weapontype Type = Weapontype.Temp;
    public Image Locked;
    public Text Cost;
    int _Cost = 0;
    bool Unlocked = false;
    void OnEnable()
    {
        MyInventory = GetComponentInParent<Inventory>();
    }
    void Start()
    {
        
        if (Type==Weapontype.Temp)
        {
            if (DataHandler.Instance.GetTempWeaponSlotStatus(WeaponID))
            {
                Unlocked = true;
                Locked.gameObject.SetActive(false);
            }
            GetComponent<Image>().sprite = GameManager.Instance.TempWeapons[WeaponID].GetComponent<TempWeapon>().UISprite;
            _Cost = GameManager.Instance.TempWeapons[WeaponID].GetComponent<TempWeapon>().Cost;
            Cost.text = _Cost.ToString()+" $";
        }
        else
        {
            if (DataHandler.Instance.GetMainWeaponSlotStatus(WeaponID))
            {
                Unlocked = true;
                Locked.gameObject.SetActive(false);
            }
            GetComponent<Image>().sprite = GameManager.Instance.Weapons[WeaponID].GetComponent<DefaultWeapon>().UISprite;
            _Cost = GameManager.Instance.Weapons[WeaponID].GetComponent<DefaultWeapon>().Cost;
            Cost.text = _Cost.ToString() + " $";
        }

    }
    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Unlocked)//Item Is not unlocked yet
        {
            itemBeingDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        if (Unlocked)
        {
            transform.position = eventData.position;
        }
        
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Unlocked)
        {
            itemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent == startParent)
            {
                transform.position = startPosition;
            }
        }
        
    }



    #endregion

    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Unlocked)// unlock Item if there is enogh coins
        {
            if (DataHandler.Instance.GetPlayerCoins() >= _Cost)
            {
                
                if (Type == Weapontype.Temp)
                {
                    MainMenu.Instance.Msgbox.Choice("Would you like to buy??", ()=> 
                    {
                        DataHandler.Instance.AddCoins(-_Cost);
                        Unlocked = true;
                        Locked.gameObject.SetActive(false);
                        //Save unlocked temp weapon 
                        DataHandler.Instance.UnlockTempWeapon(WeaponID);
                        
                    }
                    , () => { }, () => { });
                    
                }
                else
                {
                    MSGScript.Instance.Choice("Would you like to buy??",new UnityAction( () =>
                    {
                        DataHandler.Instance.AddCoins(-_Cost);
                        Unlocked = true;
                        Locked.gameObject.SetActive(false);
                        //Save unlocked Main weapon 
                        DataHandler.Instance.UnlockMainWeapon(WeaponID);
                    })
                   , new UnityAction(() => { }), new UnityAction(() => { }));
                   
                }
            }
        }
    }


    #endregion


}