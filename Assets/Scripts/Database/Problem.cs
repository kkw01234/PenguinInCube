using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem : MonoBehaviour
{
    public int id;
    public string question;
    public string picture;
    public string answer;
    public List<string> wronganswer;

    void Awake()
    {
        wronganswer = new List<string>();
    }
    public void setProblem(int id, string question, string picture, string answer, string wrong1,string wrong2, string wrong3)
    
    {
        this.id = id;
        this.question = question;
        this.picture = picture;
        this.answer = answer;
        wronganswer.Add(wrong1);
        wronganswer.Add(wrong2);
        wronganswer.Add(wrong3);
    }
}
