using Assets.Scripts.Models;
using Assets.Scripts.Services.Raycast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Pathfinding
{
   internal class PathfindingService : IPathfindingService
   {
      private readonly IRaycastService raycastService;

      private readonly int straightLineCost = 10;
      private readonly int diagonalLineCost = 14;
      private readonly float NeighbourHeightTolerance = 1.5f;

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

      public PathfindingService(IRaycastService raycastService)
      {
         this.raycastService = raycastService;
      }

      public List<Vector3> GetPath(Vector3Int start, Vector3Int destination, Grid<Cell> grid, bool useDiagonals = false)
      {
         var endpointsValid = ValidateEndpoints(start, destination, grid);

         if(endpointsValid) {
            PathfindingData data = Initialize(start, destination, grid, useDiagonals);

            // algorith
            while(data.OpenSet.Count > 0) {

               Cell currentCell = GetLowestFCostNode(data.OpenSet);
               if(currentCell.WorldPosition == data.EndCell.WorldPosition) {
                  return CalculatePath(data.EndCell);
               }

               data.OpenSet.Remove(currentCell);
               data.ClosedSet.Add(currentCell);

               var neighbourCells = ExploreNeighbours(currentCell, grid, data.Directions);
               SetupNeighbours(currentCell, neighbourCells, data);
            }

         }

         return null;
      }

      private List<Vector3> CalculatePath(Cell endCell)
      {
         var result = new List<Vector3>();
         var currentCell = endCell;
         result.Add(currentCell.WorldPosition + Vector3.up * currentCell.Height);

         while(currentCell.previousCell != null) {
            result.Add(currentCell.previousCell.WorldPosition + Vector3.up * currentCell.Height);

            currentCell = currentCell.previousCell;
         }
         result.Reverse();

         return result;
      }

      private PathfindingData Initialize(Vector3Int start, Vector3Int destination, Grid<Cell> grid, bool useDiagonals)
      {
         var data = new PathfindingData(useDiagonals);

         data.StartCell = grid.GetCell(start.x, start.z);
         data.EndCell = grid.GetCell(destination.x, destination.z);
         data.Directions = useDiagonals ? directionsWithDiagonals : directionsNoDiagonals;

         data.StartCell.gCost = 0;
         data.StartCell.hCost = CalculateDistanceCost(data.StartCell, data.EndCell);
         data.StartCell.fCost = CalculateFCost(data.StartCell);

         data.OpenSet = new List<Cell> {
               data.StartCell,
            };
         data.ClosedSet = new List<Cell>();

         return data;
      }

      private bool ValidateEndpoints(Vector3Int start, Vector3Int destination, Grid<Cell> grid)
      {
         var destinationHit = raycastService.GetGroundPoint(destination);

         if(destinationHit.ObjectHit) {
            if(grid.GetCell(start.x, start.z) != null && grid.GetCell(destination.x, destination.z) != null) {
               return true;
            }
         }
         return false;
      }

      private int CalculateFCost(Cell cell)
      {
         var fCost = cell.gCost + cell.hCost;
         return fCost;
      }

      private int CalculateDistanceCost(Cell current, Cell next)
      {
         int xDistance = Mathf.Abs(current.WorldPosition.x - next.WorldPosition.x);
         int zDistance = Mathf.Abs(current.WorldPosition.z - next.WorldPosition.z);
         int remaining = Mathf.Abs(xDistance - zDistance);

         return diagonalLineCost * Mathf.Min(xDistance, zDistance) + straightLineCost * remaining;
      }

      private Cell GetLowestFCostNode(List<Cell> cellList)
      {
         Cell min = cellList[0];
         for(int i = 1; i < cellList.Count; i++) {
            if(cellList[i].fCost < min.fCost) {
               min = cellList[i];
            }
         }

         return min;
      }

      private List<Cell> ExploreNeighbours(Cell currentCell, Grid<Cell> grid, Vector3Int[] directions)
      {
         var result = new List<Cell>();

         foreach(var direction in directions) {
            var neighbourPosition = currentCell.WorldPosition + direction * grid.CellSize;
            var neighbourCell = grid.GetCell(neighbourPosition.x, neighbourPosition.z);

            if(neighbourCell != null && VerifyNeighbourCellWalkableAndHeightTolerance(currentCell, neighbourCell)) {
               result.Add(neighbourCell);
            }
         }

         return result;
      }

      private void SetupNeighbours(Cell currentCell, List<Cell> neighbourCells, PathfindingData data)
      {
         foreach(var neighbourCell in neighbourCells) {
            if(neighbourCell != null) {

               if(data.ClosedSet.Contains(neighbourCell)) {
                  continue;
               }

               int tentativeGCost = currentCell.gCost + CalculateDistanceCost(currentCell, neighbourCell);
               if(tentativeGCost < neighbourCell.gCost) {
                  neighbourCell.previousCell = currentCell;
                  neighbourCell.gCost = tentativeGCost;
                  neighbourCell.hCost = CalculateDistanceCost(neighbourCell, data.EndCell);
                  neighbourCell.fCost = CalculateFCost(neighbourCell);

                  if(!data.OpenSet.Contains(neighbourCell)) {
                     data.OpenSet.Add(neighbourCell);
                  }
               }
            }
         }
      }

      private bool VerifyNeighbourCellWalkableAndHeightTolerance(Cell currentCell, Cell neighbourCell)
      {
         bool result = false;
         if(/*TODO: neighbourCell.IsWalkable &&*/ Mathf.Abs(currentCell.Height - neighbourCell.Height) <= NeighbourHeightTolerance) {
            result = true;
         }

         return result;
      }
   }
}
