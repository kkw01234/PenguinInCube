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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDatabase(string dbname)
    {

        string conn = "URI=file:" + dbname+".db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
    }

    public void closeDatabase()
    {
        dbconn.Close();
        dbconn = null;
    }
}
