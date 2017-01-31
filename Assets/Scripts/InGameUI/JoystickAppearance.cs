using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class JoystickAppearance : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler {
    protected class TrackedTouch
    {
        public Vector2 startPos;
        public Vector2 currentPos;
    }
    public Image Holder;
    public Joystick joystick;
    Vector2 StartPos;
    // Use this for initialization
    void Start () {
        StartPos = Holder.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        //int numTouches = Input.touchCount;
        //if (numTouches >= 1 && !Holder.enabled)
        //{
        //    Holder.enabled = true;
        //    joystick.gameObject.GetComponent<Image>().enabled = true;
        //    Touch touch = Input.touches[0];
        //    StartPos = touch.position;
        //    Holder.transform.position = touch.position;
        //    //Holder.gameObject.SetActive(true);
        //}
        //else if(numTouches<1)
        //{
        //    // Holder.gameObject.SetActive(false);
        ////    Holder.enabled = false;
        ////    joystick.gameObject.GetComponent<Image>().enabled = false;
        //}
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        joystick.OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
            Holder.enabled = false;
            joystick.gameObject.GetComponent<Image>().enabled = false;
            GameManager.Instance.IsAttacking = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.IsAttacking = true;
        Holder.enabled = true;
        joystick.gameObject.GetComponent<Image>().enabled = true;
        StartPos = eventData.position;
        Holder.transform.position = eventData.position;
    }
}
