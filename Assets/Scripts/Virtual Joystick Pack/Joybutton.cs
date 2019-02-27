using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log("jump");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
