using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static Cube instance;

    public MeshRenderer[] outsideWall;
    public TextMesh[] bottomExample;

    public TextMesh questionMesh;//문제 띄우는 변수
    public SpriteRenderer questionImage; // 문제의 이미지 띄우는 변수
    
    public Transform[] toyPos;
    public GameObject[] toy;
    
    

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        OnOffOutsideWall();
    }

    //외벽 생성 및 제거
    void OnOffOutsideWall()
    {
        if (GameManager.instance.isGameStart && outsideWall[0].isVisible) //게임 시작시 외벽제거
        {
            GenerateToy();
            for (int i = 0; i < outsideWall.Length; i++)
                outsideWall[i].enabled = false;
        }
        else if (GGUMI.instance == null && !outsideWall[0].isVisible) //게임 종료시 외벽생성
        {
            ResetToy();
            for (int i = 0; i < outsideWall.Length; i++)
                outsideWall[i].enabled = true;
        }
    }

    private void shuffleList<T>(List<T> list) // 리스트 셔플
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }

    public void loadQuestion(Problem problem) // 문제 설정
    {
        /*
        for (int i = 0; i < problem.question.Length; i+=10)
        {
            if (i % 10 == 0)
            {
                problem.question = problem.question.Insert(i, "\n");
            }
        }
        */
        questionMesh.text = problem.question;
        questionMesh.color = Color.black;
        
        if (problem.picture != "0")
        {
            Texture2D texture2D = Resources.Load<Texture2D>(problem.picture);
            Rect rect = new Rect(0, 0, texture2D.width, texture2D.height);
            questionImage.sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
        }

    }

    public int loadExample(Problem problem) //보기 설정
    {
        int answernumber = (int) Random.Range(0, 4);

        shuffleList(problem.wronganswer); // 오답도 디비에 적힌 순서와 상관 없게 랜덤으로 셔플
        bottomExample[answernumber].text = problem.answer;
        int j = 0;
        for (int i = 0; i < bottomExample.Length; ++i)
        {
            if (i == answernumber) continue;
            TextMesh text = bottomExample[i];
            text.text = problem.wronganswer[j];
            j++;
        }

        return answernumber;
    }

    public void clearExample() //보기를 완전 초기화
    {
        for (int i = 0; i < bottomExample.Length; ++i)
        {
            bottomExample[i].text = "";
        }

        questionMesh.text = "";
        questionImage.sprite = null;
        
    }

    public void GenerateToy()
    {
        for (int i = 0; i < toy.Length; i++)
        {
            Instantiate(toy[i], toyPos[i]);
        }
    }

    public void ResetToy()
    {
        if (toyPos[0].GetComponentsInChildren<Transform>().Length > 1)
        {
            for (int i = 0; i < toy.Length; i++)
            {
                Destroy(toyPos[i].GetComponentsInChildren<Transform>()[1].gameObject);
            }
        }
    }
}