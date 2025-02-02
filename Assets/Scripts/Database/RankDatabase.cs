﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class RankDatabase : MonoBehaviour
{
   
   
   public static RankDatabase instance;
   public List<Rank> rlist = null;//Rank 리스트

   void Awake()
   {
      instance = this;
      rlist = new List<Rank>();
   }


   public void getRankDatabase() //Rank에 들어있는 데이터베이스를 level은 내림차순, besttime은 오름차순으로 정렬한 것을 rlist에 저장하는 함수
   {

      rlist.RemoveRange(0,rlist.Count);
      SqlLogin.instance.OpenDatabase("Problem");
      IDbCommand dbcmd = SqlLogin.instance.dbconn.CreateCommand();

      string sqlQuery = "SELECT * FROM Rank ORDER BY level DESC, besttime ASC";

      dbcmd.CommandText = sqlQuery;

      IDataReader reader = dbcmd.ExecuteReader();
      int n=0;

      while (reader.Read())
      {
         Rank rank = new Rank();
         DateTime date = DateTime.FromFileTime(reader.GetInt64(2)); //long값을 date 값으로 변경
         rank.setRank(reader.GetInt32(0), reader.GetString(1), date, reader.GetInt32(3),reader.GetInt32(4));
         rlist.Add(rank);
         n++;
      }

       
      reader.Close();
      reader = null;
      SqlLogin.instance.CloseDatabase();

   }

   public bool insertRank(Rank rank) //rank에 들어갈만한 데이터일경우 저장하는 함수
   {
      if (compareRank(rank))//비교
      {
         
         
            SqlLogin.instance.OpenDatabase("Problem");
            IDbCommand dbcmd = SqlLogin.instance.dbconn.CreateCommand();

            string sqlQuery = "INSERT INTO Rank(name,bestdate,besttime,level) VALUES('" + rank.name + "'," + rank.bestdate.ToFileTime() +
                              "," +
                              rank.besttime + ","+rank.level+")"; //SQL문 => 데이터베이스에 쿼리 날리는 곳
            dbcmd.CommandText = sqlQuery;
         
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Close();
            reader = null;
            SqlLogin.instance.CloseDatabase();
            getRankDatabase();
            return true;
         
       
      }
      else
         return false;

   }

   public void deleteRank(Rank rank) //데이터베이스에서 안좋은 level과 시간을 가진 사람은 삭제하는 함수 (생각해보니 메서드라고 해야 맞나???)
   {
      SqlLogin.instance.OpenDatabase("Problem");
      IDbCommand dbcmd = SqlLogin.instance.dbconn.CreateCommand();
      string sqlQuery = "DELETE FROM Rank WHERE id="+rank.id;

      dbcmd.CommandText = sqlQuery;

      IDataReader reader = dbcmd.ExecuteReader();
      reader.Close();
      reader = null;
      SqlLogin.instance.CloseDatabase();
      
     

   }
   
   public bool compareRank(Rank rank) // 비교해서 현재 점수가 최악점수보다 좋으면 삭제시키고 현재 점수가 들어가는 함수
   {
      if (rlist.Count < 10)
      {
         return true;
      }
      
      Rank worstRank = rlist[rlist.Count - 1];
      if (rank.level > worstRank.level)
      {
         deleteRank(worstRank);
         return true;
      }else if (rank.level == worstRank.level)
      {
         if (rank.besttime < worstRank.besttime)
         {
            deleteRank(worstRank);
            return true;
         }
      }
             
      
      return false;
   }
   //db에서 최악의 레벨과 최악의 시간을 뽑아서 비교만 하는게 더 나은가?  
}


//Comparer를 쓰는게 나을까 디비를 불러오는게 더 나은가


/*
 *
 * 
 */