using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq.Expressions;
using Mono.Data.SqliteClient;
using UnityEditor;
using UnityEngine;

public class SqlLogin : MonoBehaviour
{
    public static SqlLogin instance;
    public IDbConnection dbconn;
    public bool updateApp;
 

    void Awake()
    {
        instance = this;
        updateApp = false;
    }

    public void OpenDatabase(string dbname) //SQL 로그인
    {
        try
        {
            string conn = CopyDB(dbname);

            dbconn = (IDbConnection) new SqliteConnection(conn);
            dbconn.Open();
        }
        catch (Exception e)
        {
            Log(e.ToString());
        }
    }

    public void CloseDatabase() // SQL 로그아웃
    {
        dbconn.Close();
        dbconn = null;
    }


    string CopyDB(string dbname)
    {
        string filepath = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            {
                filepath = Application.persistentDataPath + "/" + dbname + ".db";
                //Log("FilePath : "+filepath);
                Log(Application.dataPath+"!/assets/"+dbname+".db");
                Log(Application.streamingAssetsPath+"/"+dbname+".db");
                Log(Application.persistentDataPath+"/"+dbname+".db"); //실주소

                if (!File.Exists(filepath))
                {
                    WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + dbname + ".db");
                    Log("loadDB : " + loadDB.url);
                    loadDB.bytesDownloaded.ToString();
                    while (!loadDB.isDone)
                    {
                    }

                    File.WriteAllBytes(filepath, loadDB.bytes);
                }

                Log("Finish\n");
                return "URI=file:" + Application.persistentDataPath + "/" + dbname + ".db";
                break;
            }
            case RuntimePlatform.WindowsEditor:
            {
                Debug.Log(Application.persistentDataPath);
                Debug.Log(Application.dataPath+"!/Assets/"+dbname+".db");
                return "URI=file:" + Application.dataPath + "/StreamingAssets/" + dbname + ".db";
                break;
            }
            default :
                return null;
        }
    }

    void Log(string log)
    {
        GameManager.instance.errortext.text += log+"\n";
        if (File.Exists(log))
        {
            GameManager.instance.errortext.text += "존재합니다!!\n";
        }
        else
            return;
    }
}