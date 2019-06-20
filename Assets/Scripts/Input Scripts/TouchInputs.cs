using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputs : MonoBehaviour
{
    public VirtualJoystick joystick;
    public RectTransform boostButton;

    public float verticalInput;
    public float horizontalInput;

    public bool boostActive;

    void Update()
    {
        verticalInput = joystick.input.y;
        horizontalInput = joystick.input.x;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
