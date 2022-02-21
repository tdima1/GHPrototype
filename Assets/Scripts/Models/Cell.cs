using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
   public class Cell
   {
      public Vector3Int WorldPosition { get; set; }
      public float Height { get; set; }

      public int gCost;
      public int hCost;
      public int fCost;
      public Cell previousCell;

      public Cell()
      {
         gCost = int.MaxValue;
         fCost = gCost + hCost;
      }
   }
}
