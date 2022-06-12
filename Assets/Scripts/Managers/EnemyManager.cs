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
               TimeUntilNextSpawn = Random.Range(spawnPointMinTimeBetweenSpawns, spawnPointMaxTimeBetweenSpawns),
         };

            spawnPoints.Add(spawnPoint);
         }
      }

      private void Update()
      {
         var availableSpawnPoints = spawnPoints.Where(s => s.EntitiesSpawned < maxSpawnsPerSpawnPoint).ToList();

         foreach(var spawnPoint in availableSpawnPoints) {
            spawnPoint.TimeUntilNextSpawn -= Time.deltaTime;

            if (spawnPoint.TimeUntilNextSpawn < 0) {

               spawnPoint.TimeUntilNextSpawn = Random.Range(spawnPointMinTimeBetweenSpawns, spawnPointMaxTimeBetweenSpawns);

               int numberOfEnemiesToSpawn = Random.Range(1, maxSpawnCounterPerSpawn);

               var enemies = entitySpawnerService.SpawnEntitiesAroundSource(spawnPoint.SpawnPointTransform, numberOfEnemiesToSpawn, wanderingDistance);

               spawnPoint.Entities.AddRange(enemies);
               spawnPoint.EntitiesSpawned += numberOfEnemiesToSpawn;
            }
         }
      }
   }
}
