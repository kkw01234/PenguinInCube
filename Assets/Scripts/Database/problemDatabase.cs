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
    public void sqlProblemInfo()
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
            problem.id = reader.GetInt32(0);
            problem.question = reader.GetString(1);
            problem.picture = reader.GetString(2);
            problem.answer = reader.GetString(3);
            for (int i = 4; i < 7; ++i)
            {
                problem.wronganswer.Add(reader.GetString(i));
            }

            plist.Add(problem);
            problem = null;
            n++;
        }

        reader.Close();
        reader = null;
        login.closeDatabase();
    }
}
