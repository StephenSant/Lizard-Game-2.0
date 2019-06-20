using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TouchInputs : MonoBehaviour
{
    public VirtualJoystick joystick;

    public float verticalInput;
    public float horizontalInput;

    public bool boostActive;

    void Update()
    {
        verticalInput = joystick.input.y;
        horizontalInput = joystick.input.x;
    }

    //Connect these to a button with the trigger event component
    public void ActivateBoost()
    {
        boostActive = true;
    }
    public void DeactivateBoost()
    {
        boostActive = false;
    }

}