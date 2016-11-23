using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IDropHandler
{
    Inventory MyInventory;
    void OnEnable()
    {
        MyInventory = GetComponentInParent<Inventory>();
    }
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!item && DragHandeler.itemBeingDragged.GetComponent<DragHandeler>().MyInventory==MyInventory)
        {
            DragHandeler.itemBeingDragged.transform.SetParent(transform);
            GetComponent<Image>().sprite = null;
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
    #endregion
}