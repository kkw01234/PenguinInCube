using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Button startButton;
    public bool isGameStart = false;
    public GameObject player;
    public Material[] slimeColor;
    public GameObject[] arrows;
    public Chanel chanel;
    public Clock timer;
    public Transform respawnPoint; //게임 시작시 플레이어 리스폰 장소
    public int level;

    public Text timeRecord_best;
    public Text levelRecord_best;
    public Text timeRecord_now;
    public Text levelRecord_now;
    private int time_best = 999999;
    private int level_best = 0;
    
    private Problem presentProblem; // 현재 문제
    private bool loadProblem; //문제를 로드해줌
    private bool complete; // 문제를 맞췄을 경우
    private bool fail; // 문제를 틀렸을 경우
    private int answerNumber; // 문제의 정답

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
            Debug.Log("game Exit");
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
                int moveIndex = (answerNumber + 2) % 4;
                GGUMI.instance.transform.position = new Vector3(
                    arrows[moveIndex].transform.position.x,
                    GGUMI.instance.gameObject.transform.position.y,
                    arrows[moveIndex].transform.position.z);
                Debug.Log("You are right " + level);
                level++;
                complete = false;
                loadProblem = true;
                Cube.instance.ResetToy();
                Cube.instance.GenerateToy();
            }

            if (fail)
            {
                fail = false;
                Clear();
                Debug.Log("You are wrong " + level);
            }
        }
        else
        {
            timer.reset();
        }

        if(GGUMI.instance == null)
            startButton.gameObject.SetActive(true);
    }

    public void CheckAnswer(int answer)
    {
        if (answer == answerNumber) complete = true;
        else fail = true;
    }

    void Clear()
    {
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
        isGameStart = false;
        Cube.instance.clearExample();
    }
    
    void GetProblem()
    {
        ProblemDatabase.instance.SqlsomeProblemInfo(level);
        
    }
}