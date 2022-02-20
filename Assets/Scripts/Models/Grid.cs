using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
   public class Grid<TGridObject> where TGridObject : class
   {
      public readonly int CellSize;
      public readonly int GridWidth;
      public readonly int GridRadius;
      public Dictionary<(int, int), TGridObject> Cells;

      public Grid(int gridRadius, int cellSize)
      {
         this.GridWidth = 2 * gridRadius + 1;
         this.CellSize = cellSize;
         this.GridRadius = gridRadius;
         Cells = new Dictionary<(int, int), TGridObject>();
      }

      public TGridObject GetElementAtGridPosition(int x, int y)
      {
         if(x < GridWidth && y < GridWidth && x >= 0 && y >= 0 && Cells.ContainsKey((x,y))) {
            return Cells[(x, y)];
         } else {
            return default;
         }
      }

      public void SetElementAtGridPosition(int x, int y, TGridObject value)
      {
         if(x < GridWidth && y < GridWidth && x >= 0 && y >= 0) {
            Cells[(x, y)] = value;
         }
      }
      public void SetElementAtGridPosition(Vector3Int cellGridPosition, TGridObject value)
      {
         if(cellGridPosition.x < GridWidth && cellGridPosition.z < GridWidth && cellGridPosition.x >= 0 && cellGridPosition.z >= 0) {
            Cells[(cellGridPosition.x, cellGridPosition.z)] = value;
         }
      }
   }
}
