using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int number;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider player)
    {
        if (Player.instance.joyActionButton.isPressed)
        {
            Player.instance.joyActionButton.isPressed = false;
            GameManager.instance.checkAnswer(number);
        }
    }
}
