﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private Text timer; //타이머의 텍스트
    public int elapsedTime; //게임이 켜진 후부터 경과된 시간
    public int startTime; // 새로운 게임 시작한 시간
    public int sec, min, hour; //초, 분, 시

    void Start()
    {
        timer = GetComponent<Text>();
    }

    void Update()
    {
        Timer();
    }

    //타이머
    void Timer()
    {
        if (!GameManager.instance.isGameStart)
            startTime = (int)Time.time;
        elapsedTime = (int) Time.time - startTime;
        sec = elapsedTime - hour * 3600 - min * 60; //시간 계산
        if (sec >= 60)
        {
            sec = 0;
            min++;
        }

        if (min >= 60)
        {
            min = 0;
            hour++;
        }

        timer.text = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec); //두 자리수로 나타냄;
    }
}