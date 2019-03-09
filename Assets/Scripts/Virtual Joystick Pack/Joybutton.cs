using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour,IPointerUpHandler
{
    public bool isPressed = false;
    public String action;

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = true;
        // Debug.Log(action + " up");
    }
}
