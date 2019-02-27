using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chanel : MonoBehaviour
{
    private Text chanel;        //채널의 텍스트
    private int nowChanel = 0;  //현재 채널
    private int newChanel = 0;  //새로운 채널값

    void Start()
    {
        chanel = GetComponent<Text>();
        RandomChanel();
    }
    
    void Update()
    {
        
    }

    //채널을 랜덤하게 바꿔줌
    void RandomChanel()
    {
        while(nowChanel == newChanel)   //채널 변동이 있을때 까지 랜덤
        {
            newChanel = (int)Random.Range(0, 1000);
        }
        nowChanel = newChanel;
        chanel.text = string.Format("CH - {0:000}", nowChanel);
    }
}
