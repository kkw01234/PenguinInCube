using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Button startButton;
    public bool isGameStart = false;
    public GameObject[] player;
    public Transform respawnPoint;  //게임 시작시 플레이어 리스폰 장소
    public int level;
    public TextMesh questionMesh;
    private Problem presentProblem; // 현재 문제


    private bool loadProblem; //문제를 로드해줌
    private bool complete; // 문제를 맞췄을 경우

    private void Awake()
    {
        instance = this;
        level = 1;
        loadProblem = true;
    }

    void Start()
    {
       //
    }
    
    void Update()
    {
        //임시게임 종료 버튼
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(this.GetComponentInChildren<Player>().gameObject);
            isGameStart = false;
            startButton.gameObject.SetActive(true);
        }
        
    }


    void FixedUpdate()
    {
        if (loadProblem)
        {
            getProblem(level);
            int rand = (int) Random.Range(0,problemDatabase.instance.plist.Count);
            presentProblem = problemDatabase.instance.getQuestion(rand);
            Debug.Log(presentProblem.question);
            questionMesh.text = presentProblem.question;
            questionMesh.color = Color.black;
            Cube.instance.loadExample(presentProblem);
            loadProblem = false;
        }
        if (complete)
        {
            level++;
            complete = false;
            loadProblem = true;
        }
    }


    void getProblem(int level)
    {
        problemDatabase p = gameObject.AddComponent<problemDatabase>();
        p.sqlsomeProblemInfo(1);

    }
}
