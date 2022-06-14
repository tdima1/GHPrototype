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

      [SerializeField][Range(1,100)] private int maxEnemiesPerSpawnPoint;
      [SerializeField] [Range(1, 3)] private int maxEnemiesSpawnedAtOnce;

      [SerializeField] [Range(1, 10)] private int minTimeBetweenSpawns;
      [SerializeField] [Range(1, 30)] private int maxTimeBetweenSpawns;

      [SerializeField] [Range(1, 10)] private int wanderingDistance;

      private List<SpawnPoint> spawnPoints;

      private void Start()
      {
         InitializeSpawnPoints();
      }

      private void Update()
      {
         SpawnEnemies();
      }

      private void InitializeSpawnPoints()
      {
         spawnPoints = new List<SpawnPoint>();

         foreach(var prefab in spawnPointsPrefabs) {
            var spawnPoint = new SpawnPoint() {
               Entities = new List<Transform>(),
               EntitiesSpawned = 0,
               SpawnPointTransform = prefab,
               TimeUntilNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns),
            };

            spawnPoints.Add(spawnPoint);
         }
      }

      private void SpawnEnemies()
      {
         var availableSpawnPoints = spawnPoints.Where(s => s.EntitiesSpawned < maxEnemiesPerSpawnPoint).ToList();

         foreach(var spawnPoint in availableSpawnPoints) {
            spawnPoint.TimeUntilNextSpawn -= Time.deltaTime;

            if(spawnPoint.TimeUntilNextSpawn < 0) {

               spawnPoint.TimeUntilNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

               int numberOfEnemiesToSpawn = Random.Range(1, maxEnemiesSpawnedAtOnce);

               var enemies = entitySpawnerService.SpawnEntitiesAroundSource(spawnPoint.SpawnPointTransform, numberOfEnemiesToSpawn, wanderingDistance);

               spawnPoint.Entities.AddRange(enemies);
               spawnPoint.EntitiesSpawned += numberOfEnemiesToSpawn;
            }
         }
      }
   }
}
