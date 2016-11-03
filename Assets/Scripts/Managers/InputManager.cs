using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
public delegate void MovementChanged(float x);


public class InputManager : MonoBehaviour {
    public static event MovementChanged movementChanged;
    public static InputManager Instance;
    CrossPlatformInputManager.VirtualAxis HorizontalAxis;
    public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input

    private float m_xAxis;
    private float m_lastDirection=0;
    private bool updateHorizontal = true;
    bool RightButton = false;
    bool LeftButton = false;
    private static void OnMovementChanged(float xmovement)// Invoking the movement event
    {
        var handler = movementChanged;
        if (handler != null) handler(xmovement);
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