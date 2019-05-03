using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDropOne : MonoBehaviour
{
    private Vector3 respawnPos;
    private Vector3 ggumiPos;
    private Vector3 resetPoint;

    private void Start()
    {
        respawnPos = GameManager.instance.respawnPoint.position;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("drop!");
        ggumiPos = GGUMI.instance.transform.position;
        resetPoint = new Vector3(ggumiPos.x, respawnPos.y, ggumiPos.z);
        GGUMI.instance.transform.position = resetPoint;
        GGUMI.instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
