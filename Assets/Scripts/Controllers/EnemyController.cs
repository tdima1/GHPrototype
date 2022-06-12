using Assets.Scripts.Services.Grid;
using Assets.Scripts.Services.Movement;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
   public class EnemyController : UnitController
   {
      public Transform SpawnPoint;

      private IMovementService movementService;

      private float timeSinceLastWandering = 0f;
      private float timeUntilNextWandering = 0f;

      [Inject]
      public void Construct(IMovementService movementService)
      {
         this.movementService = movementService;
      }

      private void Start()
      {
         
      }

      private void FixedUpdate()
      {
         timeSinceLastWandering += Time.deltaTime;

         if (timeSinceLastWandering > timeUntilNextWandering) {
            Debug.Log($"{transform.name} is moving now");

            timeSinceLastWandering = Time.time;
            timeUntilNextWandering = Random.Range(5000, 10000);



            movementService.MoveUnit(transform, Vector3.zero);
         }
      }
   }
}
