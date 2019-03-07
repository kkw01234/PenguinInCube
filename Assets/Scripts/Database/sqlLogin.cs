using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;

public class sqlLogin : MonoBehaviour
{
    public static sqlLogin instance;
    public IDbConnection dbconn;
    void Awake()
    {
        instance = this;
    }

  

    public void openDatabase(string dbname) //SQL 로그인
    {

        string conn = "URI=file:" + dbname+".db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
    }

    public void closeDatabase() // SQL 로그아웃
    {
        dbconn.Close();
        dbconn = null;
    }
}
