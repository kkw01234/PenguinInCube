using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;

public class ProblemDatabase : MonoBehaviour
{
    public static ProblemDatabase instance;
    public List<Problem> plist = null;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        plist = new List<Problem>();
    }
    public void SqlAllProblemInfo() // 모든 문제들 불러 모으는 코드
    {
        plist.Clear();
        SqlLogin.instance.OpenDatabase("Problem");
        IDbCommand dbcmd = SqlLogin.instance.dbconn.CreateCommand();

        string sqlQuery = "SELECT * FROM problem";

        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        int n=0;

        while (reader.Read())
        {
            Problem problem = new Problem();
            problem.setProblem(reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetInt32(7));    
            plist.Add(problem);
            n++;
        }

        reader.Close();
        reader = null;
        SqlLogin.instance.CloseDatabase();
    }

    public void SqlsomeProblemInfo(int level)  //레벨별로 불러오는 것
    {
        plist.Clear();
        SqlLogin.instance.OpenDatabase("Problem");
        IDbCommand dbcmd = SqlLogin.instance.dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM problem WHERE level =" + level;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        int n = 0;
        while (reader.Read())
        {
            Problem problem = new Problem();
            problem.setProblem(reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetInt32(7));    
            plist.Add(problem);
            n++;
        }
        SqlLogin.instance.CloseDatabase();
    }


    public Problem GetQuestion(int id)
    {
        Problem problem = plist[id];
        return problem;
    }
}
