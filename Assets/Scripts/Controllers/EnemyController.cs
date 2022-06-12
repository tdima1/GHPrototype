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

      private float remainingTime;

      [Inject]
      public void Construct(IMovementService movementService)
      {
         this.movementService = movementService;
      }

      private void Start()
      {
         remainingTime = Random.Range(3, 10);
      }

      private void Update()
      {
         remainingTime -= Time.deltaTime;

         if (remainingTime < 0) {
            Debug.Log($"{transform.name} is moving now");

            remainingTime = Random.Range(1, 10);

            movementService.MoveUnit(transform, Vector3.zero);
         }
      }
   }
}
