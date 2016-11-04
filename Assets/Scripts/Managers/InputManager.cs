﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
public delegate void MovementChanged(float x);
public delegate void Attack(bool atk);

public class InputManager : MonoBehaviour {
    public static event MovementChanged movementChanged;
    public static event Attack attack;
    public static InputManager Instance;
    CrossPlatformInputManager.VirtualAxis HorizontalAxis;
    CrossPlatformInputManager.VirtualButton Fire;
    public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
    public string FireButtonName = "Fire";
    private float m_xAxis;
    public bool Is_attacking;
    private float m_lastDirection;
    private bool updateHorizontal = true;
    public bool ControlScheme0 = true;
    public bool ControlScheme1 = false;
    public bool ControlScheme2 = false;
    bool RightButton = false;
    bool LeftButton = false;
    bool pressed = false;
    
    private static void OnMovementChanged(float xmovement)// Invoking the movement event
    {
        var handler = movementChanged;
        if (handler != null) handler(xmovement);
    }
    private static void OnAttack(bool atk)// Invoking the movement event
    {
        var handler = attack;
        if (handler != null) handler(atk);
    }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        HorizontalAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(HorizontalAxis);
        Fire = new CrossPlatformInputManager.VirtualButton(FireButtonName);
        CrossPlatformInputManager.RegisterVirtualButton(Fire);
        if(ControlScheme0)
        {
            m_lastDirection = 0;
        }
        else if(ControlScheme1)
        {
            m_lastDirection = 1;
        }
    }
    public void UpdateHorizontalRight(float v)
    {
        if(v>0.1f)
        {
            RightButton = true;
        }
        else
        {
            RightButton = false;
        }
        
    }
    public void UpdateHorizontalLeft(float v)
    {
        if (v > 0.1f)
        {
            LeftButton = true;
        }
        else
        {
            LeftButton = false;
        }

    }
    void Update()
    {
#if UNITY_EDITOR  //platform defines Run In Unity Only

        m_xAxis = Input.GetAxis("Horizontal");
#else
        if(ControlScheme0)
        {
if(RightButton && !LeftButton)
        {
            HorizontalAxis.Update(1);

        }
else if(LeftButton && !RightButton)
        {
            HorizontalAxis.Update(-1);
        }
        
        else if(!LeftButton && !RightButton)
        {
            HorizontalAxis.Update(0);
        }
        
        }
        if(ControlScheme1)
        {
            if(RightButton && !pressed)
            {
                m_lastDirection *= -1;
                HorizontalAxis.Update(m_lastDirection);

                pressed = true;
            }
            if(!RightButton)
            {
                pressed = false;
            }

            OnAttack(LeftButton);
        }
        m_xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
#endif
        if (Mathf.Abs(m_xAxis)>0)
        {
            OnMovementChanged(m_xAxis);
        }
    }
    void OnDisable()
    {
        if (CrossPlatformInputManager.AxisExists(horizontalAxisName))
            CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);
    }
}