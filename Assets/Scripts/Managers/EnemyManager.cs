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
      [SerializeField] [Range(1, 3)] private int maxSpawnCounterPerSpawn;

      [SerializeField] [Range(1, 10)] private int spawnPointMinTimeBetweenSpawns;
      [SerializeField] [Range(1, 30)] private int spawnPointMaxTimeBetweenSpawns;

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
      }

      private void Update()
      {
         foreach (var spawnPoint in spawnPoints) {
            spawnPoint.TimeUntilNextSpawn -= Time.deltaTime;
         }

         var availableSpawnPoints = spawnPoints.Where(s => s.TimeUntilNextSpawn < 0);

         foreach (var spawnPoint in availableSpawnPoints) {
            spawnPoint.TimeUntilNextSpawn = Random.Range(spawnPointMinTimeBetweenSpawns, spawnPointMaxTimeBetweenSpawns);

            var freeSpawnPoints = spawnPoints.Where(s => s.EntitiesSpawned < maxSpawnsPerSpawnPoint).ToList();

            foreach(var point in freeSpawnPoints) {
               int numberOfEnemiesToSpawn = Random.Range(1, maxSpawnCounterPerSpawn);

               var enemies = entitySpawnerService.SpawnEntitiesAroundSource(point.SpawnPointTransform.position, numberOfEnemiesToSpawn, wanderingDistance, point.SpawnPointTransform);

               point.Entities.AddRange(enemies);
               point.EntitiesSpawned += numberOfEnemiesToSpawn;
            }
         }
         }
   }
}
