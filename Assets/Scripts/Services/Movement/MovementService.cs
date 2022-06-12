using Assets.Scripts.Services.Grid;
using Assets.Scripts.Services.Pathfinding;
using Assets.Scripts.Services.Raycast;
using System.Collections;
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
      private GameObject cellPrefab;
      private List<GameObject> worldCells = new List<GameObject>();

      private IEnumerator movementCoroutine;

      public MovementService(
         IGridBuilderService gridBuilderService,
         IPathfindingService pathfindingService)
      {
         this.gridBuilderService = gridBuilderService;
         this.pathfindingService = pathfindingService;
         cellPrefab = GameObject.FindGameObjectWithTag("cell");
      }

      public void MoveUnit(Transform unit, Vector3 destination)
      {
         List<Vector3> path = GetPath(unit, destination);

         if (path != null) {
            MoveUnit(unit, path);

            Debug_PlacePathCells(path);
         }
      }

      private List<Vector3> GetPath(Transform unit, Vector3 destination)
      {
         var unitPositionToInt = Vector3Int.RoundToInt(unit.position);
         var destinationToInt = Vector3Int.RoundToInt(destination);

         var movementGrid = gridBuilderService.BuildGrid(unitPositionToInt);

         var path = pathfindingService.GetPath(unitPositionToInt, destinationToInt, movementGrid);
         return path;
      }

      private void MoveUnit(Transform unit, List<Vector3> path)
      {
         var unitController = unit.GetComponent<UnitController>();

         if(movementCoroutine != null) {
            unitController.StopAllCoroutines();
         }

         movementCoroutine = MoveUnitThroughPath(unit, path);

         unitController.StartCoroutine(movementCoroutine);
      }

      private void Debug_PlacePathCells(List<Vector3> path)
      {
         foreach(var cell in worldCells) {
            Object.Destroy(cell);
         }
         worldCells.Clear();

         foreach(var cell in path) {
            GameObject worldCell = cellPrefab;
            worldCell.transform.position = cell + new Vector3(0, 0.01f, 0);
            worldCell.transform.localScale = new Vector3(0.95f, 0.95f, 1);

            worldCells.Add(Object.Instantiate(worldCell, GameObject.FindGameObjectWithTag("Grid").transform));
         }
      }

      private IEnumerator MoveUnitThroughPath(Transform unit, List<Vector3> path)
      {
         foreach(var position in path) {
            //if(path[0] == position) {
            //   unit.position += (position - unit.position).normalized * (position - unit.position).magnitude;
            //   continue;
            //}

            var movementThisFrame = 7.5f * Time.deltaTime;

            while((position - unit.position).magnitude > movementThisFrame) {

               unit.position += (position - unit.position).normalized * movementThisFrame;

               yield return new WaitForEndOfFrame();
            }
         }
      }
   }
}
