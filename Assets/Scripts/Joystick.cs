using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public enum AxisOption
    {
        // Options for which axes to use
        Both, // Use both
        OnlyHorizontal, // Only horizontal
        OnlyVertical // Only vertical
    }

    public int MovementRange = 100;
    public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use

    Vector3 m_StartPos;

    void OnEnable()
    {
    }

    void Start()
    {
        m_StartPos = transform.position;
    }

    void UpdateVirtualAxes(Vector3 value)
    {
        var delta = m_StartPos - value;
        delta.y = -delta.y;
        delta /= MovementRange;
        InputManager.Instance.UpdateAxis(-delta.x, delta.y);

    }


    public void OnDrag(PointerEventData data)
    {
        Vector3 newPos = Vector3.zero;

        int delta = (int)Vector2.Distance(data.position, m_StartPos);
        delta = Mathf.Clamp(delta, 0, MovementRange);
        Vector2 dir = (data.position - new Vector2(m_StartPos.x, m_StartPos.y)).normalized;
        newPos = dir * delta;
        transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
        UpdateVirtualAxes(transform.position);
    }


    public void OnPointerUp(PointerEventData data)
    {
        //transform.position = m_StartPos;
        //UpdateVirtualAxes(m_StartPos);
    }


    public void OnPointerDown(PointerEventData data) { }

    void OnDisable()
    {
        // remove the joysticks from the cross platform input
        //if (m_UseX)
        //{
        //	m_HorizontalVirtualAxis.Remove();
        //}
        //if (m_UseY)
        //{
        //	m_VerticalVirtualAxis.Remove();
        //}
    }
}
