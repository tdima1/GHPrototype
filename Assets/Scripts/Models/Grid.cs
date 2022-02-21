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

      public TGridObject GetCell(int x, int z)
      {
         if(Cells.ContainsKey((x,z))) {
            return Cells[(x, z)];
         } else {
            return default;
         }
      }

      public void SetElementAtGridPosition(int x, int z, TGridObject value)
      {
         if(x < GridWidth && z < GridWidth && x >= 0 && z >= 0) {
            Cells[(x, z)] = value;
         }
      }
      public void SetElementAtGridPosition(Vector3Int cellGridPosition, TGridObject value)
      {
         if(!Cells.ContainsKey((cellGridPosition.x, cellGridPosition.z))) {
            Cells[(cellGridPosition.x, cellGridPosition.z)] = value;
         }
      }
   }
}
