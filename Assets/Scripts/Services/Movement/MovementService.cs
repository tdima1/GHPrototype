using Assets.Scripts.Services.Grid;
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
      private readonly IRaycastService raycastService;
      private readonly IGridBuilderService gridBuilderService;
      private GameObject cellPrefab;

      public MovementService(
         IRaycastService raycastService,
         IGridBuilderService gridBuilderService)
      {
         this.raycastService = raycastService;
         this.gridBuilderService = gridBuilderService;
         cellPrefab = GameObject.FindGameObjectWithTag("cell");
      }

      public void MoveUnit(Transform unit, Vector3 destination)
      {
         var movementGrid = gridBuilderService.BuildGrid(unit.position);

         foreach(var cell in movementGrid.Cells) {
            GameObject worldCell = cellPrefab;
            worldCell.transform.position = cell.Value.WorldPosition + new Vector3(0, 0.01f, 0);
            worldCell.transform.localScale = movementGrid.CellSize * new Vector3(0.95f, 0.95f, 1);

            Object.Instantiate(worldCell, GameObject.FindGameObjectWithTag("Grid").transform);
         }
      }
   }
}
