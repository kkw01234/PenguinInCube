using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDropOne : MonoBehaviour
{
    private Vector3 respawnPos;
    private Vector3 ggumiPos;

    private void Start()
    {
        respawnPos = GameManager.instance.respawnPoint.position;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("drop!");
        ggumiPos = GGUMI.instance.transform.position;
        GGUMI.instance.transform.position = respawnPos;
        GGUMI.instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
