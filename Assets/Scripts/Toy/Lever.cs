using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animation machineAni;
    private bool isPull = true;

    private void OnTriggerStay(Collider other)
    {
        if (GGUMI.instance.joyActionButton.isPressed && !machineAni.isPlaying)
        {
            if (isPull)
            {
                machineAni.Play("Push");
                isPull = false;
            }
            else
            {
                machineAni.Play("Pull");
                isPull = true;
            }
        }
    }
}
