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
    public void UpdateHorizontal(float v)
    {
        HorizontalAxis.Update(v);

    }
    void Update()
    {
#if UNITY_EDITOR  //platform defines Run In Unity Only

        m_xAxis = Input.GetAxis("Horizontal");
        #else
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