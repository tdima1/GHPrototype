using Assets.Scripts.Models;
using Assets.Scripts.Services.Raycast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Grid
{
   internal class GridBuilderService : IGridBuilderService
   {
      private readonly GridBuilderData gridBuilderData;

      private readonly IRaycastService raycastService;

      public GridBuilderService(
         GridBuilderData gridBuilderData,
         IRaycastService raycastService)
      {
         this.gridBuilderData = gridBuilderData;
         this.raycastService = raycastService;
      }

      public Grid<Cell> BuildGrid(Vector3Int origin)
      {
         Grid<Cell> grid = new Grid<Cell>(gridBuilderData.GridRadius, gridBuilderData.CellSize);

         for(int i = 0; i <= grid.GridWidth / 2; i++) {
            for(int j = grid.GridWidth / 2 - i; j <= grid.GridWidth / 2 + i; j++) {

               var cell = BuildGridCell(new Vector3(origin.x + (i - grid.GridRadius), origin.y, origin.z + (j - grid.GridRadius)));
               var symCell = BuildGridCell(new Vector3(origin.x + ((grid.GridWidth - i - 1) - grid.GridRadius), origin.y, origin.z + (j - grid.GridRadius)));

               if (cell != null)
                  grid.SetElementAtGridPosition(cell.WorldPosition, cell);
               if (symCell != null)
                  grid.SetElementAtGridPosition(symCell.WorldPosition, symCell);
            }
         }

         return grid;
      }

      private Cell BuildGridCell(Vector3 cellOrigin)
      {
         Cell cell = new Cell();

         var result = raycastService.GetGroundPoint(cellOrigin);

         if(result.ObjectHit) {
            cell.Height = result.WorldPosition.y;
            cell.WorldPosition = Vector3Int.RoundToInt(result.WorldPosition);
            return cell;
         }

         return null;
      }
   }
}
