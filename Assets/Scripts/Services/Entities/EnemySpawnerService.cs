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

      public List<Transform> SpawnEntitiesAroundSource(Vector3 sourcePosition, int numberOfSpawns, int distanceFromSource, Transform parent)
      {
         var spawnLocations = GenerateRandomSpawnLocations(sourcePosition, numberOfSpawns, distanceFromSource);
         var enemies = new List<Transform>();

         foreach(var location in spawnLocations) {
            var enemy = SpawnEntity(parent, location);
            enemies.Add(enemy);
         }

         return enemies;
      }

      public Transform SpawnEntity(Transform parent, Vector3 spawnPoint)
      {
         var enemy = enemyFactory.Create(movementService);

         enemy.transform.position = spawnPoint;
         enemy.transform.parent = parent;
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
