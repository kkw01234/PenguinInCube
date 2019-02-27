using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private Text timer; //타이머의 텍스트
    public int elapsedTime; //게임이 켜진 후부터 경과된 시간
    private int sec, min, hour; //초, 분, 시

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
        elapsedTime = (int)Time.time;
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
        timer.text = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec); //두 자리수로 나타냄
    }
}
