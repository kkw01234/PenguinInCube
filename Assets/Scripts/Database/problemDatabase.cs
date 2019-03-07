using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;

public class problemDatabase : MonoBehaviour
{
    public static problemDatabase instance;
    public List<Problem> plist = null;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        plist = new List<Problem>();
    }
    public void sqlAllProblemInfo() // 모든 문제들 불러 모으는 코드
    {
        sqlLogin login = gameObject.AddComponent<sqlLogin>();
        login.openDatabase("Problem");
        IDbCommand dbcmd = sqlLogin.instance.dbconn.CreateCommand();

        string sqlQuery = "SELECT * FROM problem";

        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        int n=0;

        while (reader.Read())
        {
            Problem problem = gameObject.AddComponent<Problem>();
            problem.setProblem(reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetInt32(7));    
            plist.Add(problem);
            problem = null;
            n++;
        }

        reader.Close();
        reader = null;
        login.closeDatabase();
    }

    public void sqlsomeProblemInfo(int level)  //레벨별로 불러오는 것
    {
        sqlLogin login = gameObject.AddComponent<sqlLogin>();
        login.openDatabase("Problem");
        IDbCommand dbcmd = sqlLogin.instance.dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM problem WHERE level =" + level;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        int n = 0;
        while (reader.Read())
        {
            Problem problem = gameObject.AddComponent<Problem>();
            problem.setProblem(reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetInt32(7));    
            plist.Add(problem);
            problem = null;
            n++;
        }
        login.closeDatabase();
    }


    public Problem getQuestion(int id)
    {
        Problem problem = plist[id];
        return problem;
    }
}
