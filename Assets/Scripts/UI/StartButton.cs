using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class StartButton : MonoBehaviour, IPointerUpHandler
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    //시작 버튼을 눌렀을 시 플레이어 생성
    public void OnPointerUp(PointerEventData eventData)
    {
        Instantiate(gameManager.player, gameManager.respawnPoint.position, Quaternion.LookRotation(new Vector3(0, 0, -1)));
        gameManager.player.GetComponentInChildren<MeshRenderer>().material = gameManager.slimeColor[Random.Range(0, gameManager.slimeColor.Length)];
        gameManager.isGameStart = true;
        gameObject.SetActive(false);
    }
}