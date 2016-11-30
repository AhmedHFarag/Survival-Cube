using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    void OnEnable()
    {
        MyInventory = GetComponentInParent<Inventory>();
    }
    void Start()
    {
        if (Type==Weapontype.Temp)
        {
            GetComponent<Image>().sprite = GameManager.Instance.TempWeapons[WeaponID].GetComponent<TempWeapon>().UISprite;
        }
        else
        {
            GetComponent<Image>().sprite = GameManager.Instance.Weapons[WeaponID].GetComponent<DefaultWeapon>().UISprite;
        }

    }
    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }

    #endregion



}