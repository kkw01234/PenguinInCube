using UnityEngine;

public class Foot : MonoBehaviour
{
    public bool isGround = false;   //발이 땅에 닿아있는지?

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
            isGround = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
            isGround = false;
    }
}
