using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool isGameStart = false;
    public GameObject player; //플레이어
    public Material[] slimeColor; //슬라임의 색
    public Transform respawnPoint; //게임 시작시 플레이어 리스폰 장소

    public ParticleSystem[] fireworks; //문제 정답시 축포
    public Chanel chanel; //ui
    public Clock timer; //ui

    public GameObject overPanel; //게임 종료화면
    public Text timeRecord_best;
    public Text levelRecord_best;
    public Text timeRecord_now;
    public Text levelRecord_now;
    private int time_best = 999999;
    private int level_best = 0;

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

    public void CheckAnswer(int answer)
    {
        if (answer == answerNumber) complete = true;
        else fail = true;
    }

    void Clear()
    {
        //나중에 지울 코드
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
        
        
        
        
        level = 1;
        loadProblem = true;
        overPanel.SetActive(true);
        isGameStart = false;
        Cube.instance.clearExample();
    }
    
    void GetProblem()
    {
        ProblemDatabase.instance.SqlsomeProblemInfo(level);
        
    }
}