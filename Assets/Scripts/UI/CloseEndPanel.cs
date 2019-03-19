﻿using UnityEngine;
using UnityEngine.EventSystems;

public class CloseEndPanel : MonoBehaviour, IPointerUpHandler
{
    public GameObject startButton;
    public GameObject overPanel;

    //버튼을 눌렀을 때 패널을 끄고 시작 버튼 활성화
    public void OnPointerUp(PointerEventData eventData)
    {
        if(GGUMI.instance == null)
        {
            overPanel.SetActive(false);
            startButton.SetActive(true);
            GameManager.instance.levelRecord_best.text = "";
        }
    }
}
