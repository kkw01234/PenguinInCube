using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionText : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestionText instance;
    public TextMesh textMesh;


    private bool changeQuestion;
    void Awake()
    {
        instance = this;
        
    }


    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (changeQuestion)
        {
            int rand = (int) Random.Range(0,ProblemDatabase.instance.plist.Count);
        }
    }
    
    
}
