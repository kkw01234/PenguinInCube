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
        Instantiate(gameManager.player[Random.Range(0, gameManager.player.Length)], gameManager.respawnPoint.position,
            Quaternion.Euler(0, 0, 0));
        gameManager.isGameStart = true;
        gameObject.SetActive(false);
    }
}