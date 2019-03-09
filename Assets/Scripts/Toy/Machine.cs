using UnityEngine;

public class Machine : MonoBehaviour
{
    public MeshRenderer screen;
    public Material[] screenColor;
    private bool isButtonRed = false;

    private void OnTriggerStay(Collider other)
    {
        if (GGUMI.instance.joyActionButton.isPressed && !GetComponent<Animation>().isPlaying)
        {
            if (isButtonRed)
            {
                GetComponent<Animation>().Play("YellowButton");
                screen.material = screenColor[0];
                isButtonRed = false;
            }
            else
            {
                GetComponent<Animation>().Play("RedButton");
                screen.material = screenColor[1];
                isButtonRed = true;
            }
        }
    }
}
