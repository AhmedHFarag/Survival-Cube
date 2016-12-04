using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
public delegate void MovementChanged(float x,float y);
public delegate void Attack(bool atk);

public class InputManager : MonoBehaviour {
    public static event MovementChanged movementChanged;
    public static event Attack attack;
    public static InputManager Instance;
    CrossPlatformInputManager.VirtualAxis HorizontalAxis;
    CrossPlatformInputManager.VirtualAxis VerticalAxis;
    CrossPlatformInputManager.VirtualButton Fire;
    public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
    public string verticalAxisName = "Vertical";
    public string FireButtonName = "Fire";
    private float m_xAxis;
    private float m_yAxis;
    private float m_lastDirection;
    private bool updateHorizontal = true;
    public bool ControlSchemeArrows = true;
    public bool ControlSchemeSingleButton = false;
    public bool ControlSchemeTouch = false;
    public bool ControlSchemeJoyStick = false;
    bool RightButton = false;
    bool LeftButton = false;
    bool pressed = false;
    
    private static void OnMovementChanged(float xmovement , float ymovement)// Invoking the movement event
    {
        var handler = movementChanged;
        if (handler != null) handler(xmovement, ymovement);
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
        if (ControlSchemeArrows || ControlSchemeSingleButton)
        {
            InitialiseControls();
        }
        if(ControlSchemeArrows)
        {
            m_lastDirection = 0;
        }
        else if(ControlSchemeSingleButton)
        {
            m_lastDirection = 1;
        }
    }
    public void InitialiseControls()
    {
        HorizontalAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(HorizontalAxis);
        VerticalAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(VerticalAxis);
        Fire = new CrossPlatformInputManager.VirtualButton(FireButtonName);
        CrossPlatformInputManager.RegisterVirtualButton(Fire);
    }
    public void UnsubscripeControls()
    {
        if (CrossPlatformInputManager.AxisExists(horizontalAxisName))
            CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);
        if (CrossPlatformInputManager.AxisExists(verticalAxisName))
            CrossPlatformInputManager.UnRegisterVirtualAxis(verticalAxisName);
        if (CrossPlatformInputManager.ButtonExists(FireButtonName))
            CrossPlatformInputManager.UnRegisterVirtualButton(FireButtonName);
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
        m_yAxis = Input.GetAxis("Vertical");
#else
        if(ControlSchemeArrows)
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
        if(ControlSchemeSingleButton)
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
        m_yAxis = CrossPlatformInputManager.GetAxis("Vertical");

        
#endif
        if (ControlSchemeTouch)
        {
            m_xAxis = 0;
            m_yAxis = 0;
        }
        //m_xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        //m_yAxis = CrossPlatformInputManager.GetAxis("Vertical");
        if (Mathf.Abs(m_xAxis)>=0 || Mathf.Abs(m_yAxis) >= 0)
        {
            OnMovementChanged(m_xAxis, m_yAxis);
        }
    }
    public void UpdateAxis(float x,float y)
    {
        HorizontalAxis.Update(x);
        VerticalAxis.Update(y);
    }
    void OnDisable()
    {
        UnsubscripeControls();
    }
}