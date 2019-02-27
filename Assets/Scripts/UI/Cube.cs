using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer[] outsideWall;

    void Update()
    {
        OnOffOutsideWall();
    }

    //외벽 생성 및 제거
    void OnOffOutsideWall()
    {
        if(GameManager.instance.isGameStart && outsideWall[0].isVisible)    //게임 시작시 외벽제거
        {
            for (int i = 0; i < outsideWall.Length; i++)
                outsideWall[i].enabled = false;
        }
        else if(!GameManager.instance.isGameStart && !outsideWall[0].isVisible) //게임 종료시 외벽생성
        {
            for (int i = 0; i < outsideWall.Length; i++)
                outsideWall[i].enabled = true;
        }
    }
}
