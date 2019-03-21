using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool isGameStart = false;
    public GameObject player; //플레이어
    public Material[] slimeColor; //슬라임의 색
    public Sprite[] slimeImage; //슬라임의 색
    public Transform respawnPoint; //게임 시작시 플레이어 리스폰 장소

    public ParticleSystem[] fireworks; //문제 정답시 축포
    public Chanel chanel; //ui
    public Clock timer; //ui

    public GameObject overPanel; //게임 종료화면
    public Text timeRecord_best;
    public Text levelRecord_best;
    public Text numRecord_now;
    public Text timeRecord_now;
    public Text levelRecord_now;
    private int time_best = 999999;
    private int level_best = 0;
    public Image ggumi;

    public int level;
    public GameObject[] arrows; //선택지 화살표
    private Problem presentProblem; // 현재 문제
    public bool loadProblem; //문제를 로드해줌
    private bool complete; // 문제를 맞췄을 경우
    private bool fail; // 문제를 틀렸을 경우
    public int answerNumber; // 문제의 정답

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        level = 1;
        loadProblem = true;
        RankDatabase rankDatabase = gameObject.AddComponent<RankDatabase>();
        RankDatabase.instance.getRankDatabase();
    }

    void Update()
    {
        //임시게임 종료 버튼
        if (Input.GetKeyDown(KeyCode.X) && isGameStart)
        {
            Clear();
        }

        if (isGameStart)
        {
            if (loadProblem)
            {
                GetProblem();
                chanel.RandomChanel();
                int rand = (int) Random.Range(0, ProblemDatabase.instance.plist.Count);
                // Debug.Log("case : " + rand);
                presentProblem = ProblemDatabase.instance.GetQuestion(rand);
                // Debug.Log(presentProblem.question);
                Cube.instance.loadQuestion(presentProblem);
                answerNumber = Cube.instance.loadExample(presentProblem);
                // Debug.Log(answerNumber);
                loadProblem = false;
            }

            if (complete)
            {
                for(int i =0; i < fireworks.Length; i++)
                {
                    fireworks[i].Play();
                }
                //여기있던 코드 일부 Arrow.cs로 옮김
                Debug.Log("You are right " + level);
                level++;
                complete = false;
            }

            if (fail)
            {
                fail = false;
                Clear();
                Debug.Log("You are wrong " + level);
            }
        }
    }

    public void CheckAnswer(int answer) //답체크 함수
    {
        if (answer == answerNumber) complete = true;
        else fail = true;
    }

    void Clear() //초기화 함수
    {
        //나중에 지울 코드
        /*
        if (level > level_best)
        {
            level_best = level;
            levelRecord_best.text = string.Format("No. {0:00}", level);
            time_best = 999999;
            if ((timer.hour * 3600 + timer.min * 60 + timer.sec) < time_best)
            {
                time_best = timer.hour * 3600 + timer.min * 60 + timer.sec;
                timeRecord_best.text = string.Format("{0:00}:{1:00}:{2:00}", timer.hour, timer.min, timer.sec); //두 자리수로 나타냄
            }
        }
        else if (level == level_best)
        {
            if ((timer.hour * 3600 + timer.min * 60 + timer.sec) < time_best)
            {
                time_best = timer.hour * 3600 + timer.min * 60 + timer.sec;
                timeRecord_best.text = string.Format("{0:00}:{1:00}:{2:00}", timer.hour, timer.min, timer.sec); //두 자리수로 나타냄
            }
        }
        timeRecord_now.text = string.Format("{0:00}:{1:00}:{2:00}", timer.hour, timer.min, timer.sec); //두 자리수로 나타냄
        levelRecord_now.text = string.Format("No. {0:00}", level);
        
        */
        gameOverRecord();
      
        
        //초기화 코드
        level = 1;
        loadProblem = true;
        overPanel.SetActive(true);
        isGameStart = false;
        Cube.instance.clearExample();

        //게임 종료 화면에서 레포트 안의 슬라임 색 설정
        for (int i =0; i < slimeColor.Length; i++)
        {
            if (GGUMI.instance.GetComponentInChildren<MeshRenderer>().material.name == slimeColor[i].name +" (Instance)")
                ggumi.sprite = slimeImage[i];
        }
    }

    void GetProblem()
    {
        ProblemDatabase.instance.SqlsomeProblemInfo(level);
        
    }

    void gameOverRecord() //게임 종료시 종료 화면 데이터 올리는 함수
    {
        Rank nowrank = new Rank(0,chanel.setnowChanel().ToString(),DateTime.Today,timer.elapsedTime,level); //id 0은 임의의값임 (Insert 할 때는 필요 없는 값)
       
        RankDatabase.instance.insertRank(nowrank);

        //timerRecord_now.text => 궁금한게 있는데 이거 왜 분리 해놨어????? 붙여서 해놓으면 간격이 안맞잖아 길이 달라지면
        timeRecord_now.text = string.Format("{0:00}:{1:00}:{2:00}", timer.hour, timer.min, timer.sec);
        numRecord_now.text = string.Format("No. {0:00}", nowrank.name);
        levelRecord_now.text = string.Format("LV. {0:00} ", nowrank.level);

        int num = RankDatabase.instance.rlist.Count;
        if (num > 5)
        {
            num = 5;
        }
        for (int i = 0; i < num; ++i) //UI 수정해야할 부분
        {
            Rank rank = RankDatabase.instance.rlist[i];
            levelRecord_best.text += string.Format("No. {0:00} LV. {1:00} ", rank.name,rank.level)+string.Format("{0:00}:{1:00}:{2:00}", rank.besttime/3600, (rank.besttime/60)%60, rank.besttime%60)+"\n";
            
        }
    }
}