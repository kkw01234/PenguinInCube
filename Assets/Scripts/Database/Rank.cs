using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
   public int id;
   public string name;
   public DateTime bestdate;
   public DateTime besttime;
   public int level;


   public Rank()
   {
      
   }

   public Rank(int id, string name, DateTime bestdate, DateTime besttime, int level)
   {
      this.id = id;
      this.name = name;
      this.bestdate = bestdate;
      this.besttime = besttime;
      this.level = level;
   }
   public void setRank(int id, string name, DateTime bestdate,DateTime besttime,int level)
   {
      this.id = id;
      this.name = name;
      this.bestdate = bestdate;
      this.besttime = besttime;
      this.level = level;
   }
   
}
