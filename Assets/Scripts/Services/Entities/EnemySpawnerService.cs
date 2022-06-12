using Assets.Scripts.Controllers;
using Assets.Scripts.Factories;
using Assets.Scripts.Services.Movement;
using Assets.Scripts.Services.Raycast;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Entities
{
   internal class EnemySpawnerService : IEntitySpawnerService
   {
      private readonly IRaycastService raycastService;
      private readonly IMovementService movementService;
      private readonly EnemyFactory enemyFactory;

      public EnemySpawnerService(
         IRaycastService raycastService,
         IMovementService movementService,
         EnemyFactory enemyFactory)
      {
         this.raycastService = raycastService;
         this.movementService = movementService;
         this.enemyFactory = enemyFactory;
      }

      public List<Transform> SpawnEntitiesAroundSource(Vector3 spawnPosition, int numberOfSpawns, int wanderingDistance, Transform spawnPoint)
      {
         var spawnLocations = GenerateRandomSpawnLocations(spawnPosition, numberOfSpawns, wanderingDistance);
         var enemies = new List<Transform>();

         foreach(var location in spawnLocations) {
            var enemy = SpawnEntity(spawnPoint, location, wanderingDistance);
            enemies.Add(enemy);
         }

         return enemies;
      }

      public Transform SpawnEntity(Transform spawnPoint, Vector3 spawnPosition, int wanderingDistance)
      {
         var enemy = enemyFactory.Create(movementService);

         enemy.transform.position = spawnPosition;
         enemy.transform.parent = spawnPoint;

         enemy.WanderingDistance = wanderingDistance;
         enemy.SpawnPoint = spawnPoint;

         return enemy.transform;
      }

      public void DespawnEntity(Transform entity)
      {
         GameObject.Destroy(entity);
      }

      private List<Vector3> GenerateRandomSpawnLocations(Vector3 sourcePosition, int numberOfSpawns, int distanceFromSource)
      {
         var spawnLocations = new List<Vector3>();

         for(int i = 0; i < numberOfSpawns; i++) {
            var randX = Random.Range(sourcePosition.x - distanceFromSource, sourcePosition.x + distanceFromSource);
            var randZ = Random.Range(sourcePosition.z - distanceFromSource, sourcePosition.z + distanceFromSource);

            var groundHit = raycastService.GetGroundPoint(new Vector3(randX, 0, randZ));

            if(groundHit.ObjectHit) {
               spawnLocations.Add(groundHit.GroundPosition);
            }
         }

         return spawnLocations;
      }
   }
}
