using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
   public class PathfindingData
   {
      public Cell StartCell;
      public Cell EndCell;

      public Vector3Int[] Directions;

      public List<Cell> OpenSet = new List<Cell>();
      public List<Cell> ClosedSet = new List<Cell>();

      public PathfindingData(bool useDiagonals = false)
      {
         Directions = useDiagonals ? directionsWithDiagonals : directionsNoDiagonals;
      }

      private readonly Vector3Int[] directionsNoDiagonals = new Vector3Int[] {
         new Vector3Int(0,0,1),
         new Vector3Int(0,0,-1),
         Vector3Int.right,
         Vector3Int.left,
      };

      private readonly Vector3Int[] directionsWithDiagonals = new Vector3Int[] {
         new Vector3Int(0,0,1),
         new Vector3Int(0,0,-1),
         Vector3Int.right,
         Vector3Int.left,
         new Vector3Int(0,0,1) + Vector3Int.right,
         new Vector3Int(0,0,1) + Vector3Int.left,
         new Vector3Int(0,0,-1) + Vector3Int.right,
         new Vector3Int(0,0,-1) + Vector3Int.left,
      };
   }
}
