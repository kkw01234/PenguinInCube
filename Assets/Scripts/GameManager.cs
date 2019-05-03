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
    public Text[] record_best;
    public Text numRecord_now;
    public Text levelRecord_now;
    public Text timeRecord_now;
    public Image ggumi;
    public Text reportText; //단계별로 보고서의 글이 달라짐
    public string[] intEquivalent;
    public GameObject bestRecordText;
    public GameObject nowRecordText;

    public Text errortext;

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
            Clear();

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

        //현재 기록 입력
        numRecord_now.text = string.Format("No. {0:000}", nowrank.name);
        levelRecord_now.text = string.Format("LV. {0:00} ", nowrank.level);
        timeRecord_now.text = string.Format("{0:00}:{1:00}:{2:00}", timer.hour, timer.min, timer.sec);

        //최고 기록 top5 입력
        int num = RankDatabase.instance.rlist.Count;
        if (num > 5)
        {
            num = 5;
        }
        for (int i = 0; i < num; ++i) //UI 수정해야할 부분
        {
            Rank rank = RankDatabase.instance.rlist[i];
            record_best[i].text = string.Format("No. {0:000} LV. {1:00} ", rank.name, rank.level)
                                + string.Format("{0:00}:{1:00}:{2:00}", rank.besttime / 3600, (rank.besttime / 60) % 60, rank.besttime % 60);
            if (RankCompare(rank,nowrank))
            {
                nowRecordText.SetActive(true);
                nowRecordText.GetComponent<RectTransform>().position = record_best[i].GetComponent<RectTransform>().position + new Vector3(-113, 0, 0);
                if (i==0)
                    bestRecordText.SetActive(true);
            }
        }
        
        //보고서 멘트
        if (nowrank.level == 1) //미생물 단계
            reportText.text = String.Format("이 꾸미의 지능은 {0} 수준이다. 벌레만도 못하다...", intEquivalent[Random.Range(0, 6)]);
        else if (nowrank.level == 2) //조류 단계
            reportText.text = String.Format("이 꾸미의 지능은 {0} 수준이다. 방향 정도는 알고 있는 것 같다.", intEquivalent[Random.Range(6, 11)]);
        else if (nowrank.level == 3) //영장류 단계
            reportText.text = String.Format("이 꾸미의 지능은 {0} 수준이다. 멍청하지는 않은 것 같다.", intEquivalent[Random.Range(11, 16)]);
        else if (nowrank.level == 4) //사람 단계
            reportText.text = String.Format("이 꾸미의 지능은 {0} 수준이다. 우리와 의사소통도 가능할 것 같다.", intEquivalent[Random.Range(16, 19)]);
        else if (nowrank.level == 5) //응원 단계
            reportText.text = String.Format("이 꾸미의 지능은 훌륭하다. {0}.", intEquivalent[Random.Range(19, 21)]);
    }

    bool RankCompare(Rank rank1, Rank rank2)
    {
        if ((rank1.name == rank2.name) && (rank1.level == rank2.level) && (rank1.besttime == rank2.besttime))
            return true;
        else
            return false;
    }
}