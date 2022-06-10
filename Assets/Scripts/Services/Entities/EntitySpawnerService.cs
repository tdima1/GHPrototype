using Assets.Scripts.Services.Raycast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Entities
{
   internal class EntitySpawnerService : IEntitySpawnerService
   {
      private readonly IRaycastService raycastService;
      private readonly RaycastConstants raycastConstants;

      public EntitySpawnerService(
         IRaycastService raycastService,
         RaycastConstants raycastConstants)
      {
         this.raycastService = raycastService;
         this.raycastConstants = raycastConstants;
      }

      public Transform SpawnEntity(Transform entity, Transform parent, Vector3 spawnPosition)
      {
         return GameObject.Instantiate(entity, spawnPosition, Quaternion.identity, parent).transform;
      }

      public List<Transform> SpawnEntitiesAroundSource(Vector3 sourcePosition, Transform entity, Transform parent)
      {
         var spawnLocations = GenerateRandomSpawnLocations(sourcePosition);
         var enemies = new List<Transform>();

         foreach(var location in spawnLocations) {
            var enemy = SpawnEntity(entity, parent, location);
            enemies.Add(enemy);
         }

         return enemies;
      }

      public void DespawnEntity(Transform entity)
      {
         GameObject.Destroy(entity);
      }

      private List<Vector3> GenerateRandomSpawnLocations(Vector3 sourcePosition)
      {
         var spawnLocations = new List<Vector3>();

         for(int i = 0; i < 10; i++) {
            var randX = UnityEngine.Random.Range(sourcePosition.x - 10, sourcePosition.x + 10);
            var randZ = UnityEngine.Random.Range(sourcePosition.z - 10, sourcePosition.z + 10);

            var groundHit = raycastService.GetGroundPoint(new Vector3(randX, 0, randZ));

            if(groundHit.ObjectHit) {
               spawnLocations.Add(groundHit.GroundPosition);
            }
         }

         return spawnLocations;
      }
   }
}
