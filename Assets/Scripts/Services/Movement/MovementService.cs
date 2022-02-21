using Assets.Scripts.Services.Grid;
using Assets.Scripts.Services.Pathfinding;
using Assets.Scripts.Services.Raycast;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Movement
{
   internal class MovementService : IMovementService
   {
      private readonly IGridBuilderService gridBuilderService;
      private readonly IPathfindingService pathfindingService;
      private readonly GridBuilderData gridBuilderData;
      private GameObject cellPrefab;

      public MovementService(
         IGridBuilderService gridBuilderService,
         IPathfindingService pathfindingService,
         GridBuilderData gridBuilderData)
      {
         this.gridBuilderService = gridBuilderService;
         this.pathfindingService = pathfindingService;
         this.gridBuilderData = gridBuilderData;
         cellPrefab = GameObject.FindGameObjectWithTag("cell");
      }

      public void MoveUnit(Transform unit, Vector3 destination)
      {
         var unitPositionToInt = Vector3Int.RoundToInt(unit.position);
         var destinationToInt = Vector3Int.RoundToInt(destination);

         var movementGrid = gridBuilderService.BuildGrid(unitPositionToInt);

         var path = pathfindingService.GetPath(unitPositionToInt, destinationToInt, movementGrid);

         foreach(var cell in path) {
            GameObject worldCell = cellPrefab;
            worldCell.transform.position = cell + new Vector3(0, 0.01f, 0);
            worldCell.transform.localScale = movementGrid.CellSize * new Vector3(0.95f, 0.95f, 1);

            Object.Instantiate(worldCell, GameObject.FindGameObjectWithTag("Grid").transform);
         }
      }
   }
}
