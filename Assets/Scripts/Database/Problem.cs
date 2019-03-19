using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem //문제 bean (객체)
{
    public int id;
    public string question;
    public string picture;
    public string answer;
    public List<string> wronganswer = new List<string>();
    public int level;
    public string explanation;

    public void setProblem(int id, string question, string picture, string answer, string wrong1, string wrong2,
        string wrong3, int level,string explanation)
    {
        this.id = id;
        this.question = question;
        this.picture = picture;
        this.answer = answer;
        wronganswer.Add(wrong1);
        wronganswer.Add(wrong2);
        wronganswer.Add(wrong3);
        this.level = level;
        this.explanation = explanation;
    }
}