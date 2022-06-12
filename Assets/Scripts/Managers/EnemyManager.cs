using Assets.Scripts.Controllers;
using Assets.Scripts.Factories;
using Assets.Scripts.Models.Spawning;
using Assets.Scripts.Services.Entities;
using Assets.Scripts.Services.Movement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
   public class EnemyManager : MonoBehaviour
   {
      [Inject] private readonly IEntitySpawnerService entitySpawnerService;

      [SerializeField] private Transform enemyParent;
      [SerializeField] private List<Transform> spawnPointsPrefabs;

      [SerializeField] [Range(1, 20)] private int maxSpawnsPerMap;
      [SerializeField][Range(1,10)] private int maxSpawnsPerSpawnPoint;
      [SerializeField] [Range(1, 3)] private int maxSpawnsPerSpawn;
      [SerializeField] [Range(1, 10)] private int wanderingDistance;

      private List<SpawnPoint> spawnPoints;

      private void Start()
      {
         spawnPoints = new List<SpawnPoint>();
         maxSpawnsPerMap = Mathf.Clamp(maxSpawnsPerMap, maxSpawnsPerSpawnPoint, maxSpawnsPerSpawnPoint * spawnPointsPrefabs.Count);

         foreach (var prefab in spawnPointsPrefabs) {
            var spawnPoint = new SpawnPoint() {
               Entities = new List<Transform>(),
               EntitiesSpawned = 0,
               SpawnPointTransform = prefab,
            };

            spawnPoints.Add(spawnPoint);
         }

         //foreach(var spawnPoint in spawnPointsPrefabs) {
         //   var enemyBatch = entitySpawnerService.SpawnEntitiesAroundSource(spawnPoint.position, Random.Range(1, maxSpawnsPerSpawnPoint), initialDistanceFromSource, enemyParent);

         //   foreach(var enemy in enemyBatch) {
         //      enemy.GetComponent<EnemyController>().SpawnPoint = spawnPoint;
         //   }

         //   enemies.AddRange(enemyBatch);
         //}
      }

      private void Update()
      {
         if (spawnPoints.Select(s => s.EntitiesSpawned).Sum() <= maxSpawnsPerMap) {

            var availableSpawnPoints = spawnPoints.Where(s => s.EntitiesSpawned < maxSpawnsPerSpawnPoint).ToList();

            foreach(var spawnPoint in availableSpawnPoints) {
               int numberOfEnemiesSpawned = Random.Range(1, maxSpawnsPerSpawn);

               var enemies = entitySpawnerService.SpawnEntitiesAroundSource(spawnPoint.SpawnPointTransform.position, numberOfEnemiesSpawned, wanderingDistance, spawnPoint.SpawnPointTransform);
               
               spawnPoint.Entities.AddRange(enemies);
               spawnPoint.EntitiesSpawned += numberOfEnemiesSpawned;
            }


         }
      }
   }
}
