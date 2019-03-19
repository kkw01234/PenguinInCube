using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class RankDatabase : MonoBehaviour
{
   /*
    *미완성이에유 아직 건드리지 마세요ㅠㅠㅠㅠㅠㅠ
    *
    * */
   
   public static RankDatabase instance;
   public List<Rank> rlist = null;

   void Awake()
   {
      instance = this;
      rlist = new List<Rank>();
   }


   public void getRankDatabase() //Rank에 들어있는 데이터베이스를 level은 내림차순, besttime은 오름차순으로 정렬함
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
         DateTime date = DateTime.FromFileTime(reader.GetInt64(2));
         rank.setRank(reader.GetInt32(0), reader.GetString(1), date, reader.GetInt32(3),reader.GetInt32(4));
         rlist.Add(rank);
         n++;
      }

       
      reader.Close();
      reader = null;
      SqlLogin.instance.CloseDatabase();

   }

   public bool insertRank(Rank rank) //rank에 들어갈만한 데이터이면 들어감
   {
      if (compareRank(rank))//비교
      {
         
         
            SqlLogin.instance.OpenDatabase("Problem");
            IDbCommand dbcmd = SqlLogin.instance.dbconn.CreateCommand();

            string sqlQuery = "INSERT INTO Rank(name,bestdate,besttime,level) VALUES('" + rank.name + "'," + rank.bestdate.ToFileTime() +
                              "," +
                              rank.besttime + ","+rank.level+")";
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

   public void deleteRank(Rank rank) //데이터베이스에서 안좋은 level과 시간을 가진 사람은 삭제
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
   
   public bool compareRank(Rank rank) // 비교해서 현재 점수가 최악점수보다 좋으면 삭제시키고 현재 점수가 들어감
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

//내장함수

/*
 * 데이터베이스에 10개의 rank값을 저장
 * 10개가 넘어 갈경우 비교(1.LV 2.시간)해서 최악의 값을 디비에서 삭제
 * 근데 디비가 이미 받아져있는데 비교해서 삭제하고 넣을려고 할때 최악은 삭제되지만 넣을 Rank는 맨 마지막에 들어가게됨(디비랑 rlist)
 * 디비에서는 ASC DESC로 빼내면 되기 때문에 상관이 없음 하지만 rlist값은 정렬되지 않음 -> 나중에 비교할 때 문제가 발생
 * 그래서 rlist값을 정렬 해줘야한다 (Sort(Comparer comparer));
 *
 * 하지만 그냥 다시 디비 값을 SELECT값으로 불러온다면 rlist를 정렬할 필요가 없음 -> rlist.sort는 병합이면 O(nlogn) 디비 불러오는 O(n) , Array delete O(n)
 * 근데 어떤게 빠른지 모르겠음..............
 * 
 * 
 */