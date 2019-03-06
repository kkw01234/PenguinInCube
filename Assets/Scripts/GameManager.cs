using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStart = true;
    public GameObject[] player;
    public Transform respawnPoint;  //게임 시작시 플레이어 리스폰 장소

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        getProblem();
    }
    
    void Update()
    {
        //꿀잼
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(player[Random.Range(0,player.Length)], respawnPoint.position, Quaternion.Euler(0, 0, 0), transform);
        }
    }


    void getProblem()
    {
        problemDatabase p = gameObject.AddComponent<problemDatabase>();
        p.sqlProblemInfo();
        Debug.Log(p.plist[0]);
    }
}
