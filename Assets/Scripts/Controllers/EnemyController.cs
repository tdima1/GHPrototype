using Assets.Scripts.Services.Grid;
using Assets.Scripts.Services.Movement;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
   public class EnemyController : UnitController
   {
      public Transform SpawnPoint;
      public int WanderingDistance;

      public int minTimeWandering;
      public int maxTimeWandering;

      private IMovementService movementService;

      private float remainingTime;

      [Inject]
      public void Construct(IMovementService movementService)
      {
         this.movementService = movementService;
      }

      private void Start()
      {
         remainingTime = Random.Range(minTimeWandering, maxTimeWandering);
      }

      private void Update()
      {
         WanderAroundSpawnPoint();
      }

      private void WanderAroundSpawnPoint()
      {
         remainingTime -= Time.deltaTime;

         if(remainingTime < 0) {

            remainingTime = Random.Range(minTimeWandering, maxTimeWandering);

            var detination = new Vector3(
               SpawnPoint.position.x + Random.Range(-WanderingDistance, WanderingDistance),
               SpawnPoint.position.y,
               SpawnPoint.position.z + Random.Range(-WanderingDistance, WanderingDistance));

            movementService.MoveUnit(transform, detination);
         }
      }
   }
}
