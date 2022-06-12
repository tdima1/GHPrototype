using Assets.Scripts.Controllers;
using Assets.Scripts.Factories;
using Assets.Scripts.Services.Entities;
using Assets.Scripts.Services.Movement;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
   public class EnemyManager : MonoBehaviour
   {
      [Inject] private readonly IEntitySpawnerService entitySpawnerService;

      [SerializeField] private Transform enemyParent;
      [SerializeField] private List<Transform> spawnPoints;

      [SerializeField][Range(0,10)] private int numberOfSpawns;
      [SerializeField] [Range(1, 7)] private int initialDistanceFromSource;
      [SerializeField] [Range(1, 5)] private int maxEnemiesPerSpawn;

      private List<Transform> enemies = new List<Transform>();

      private float timeFromLastWandering = 0f;
      private float timeUntilNextWandering = 0f;

      private void Start()
      {
         foreach(var spawnPoint in spawnPoints) {
            var enemyBatch = entitySpawnerService.SpawnEntitiesAroundSource(spawnPoint.position, numberOfSpawns, initialDistanceFromSource, enemyParent);

            foreach(var enemy in enemyBatch) {
               enemy.GetComponent<EnemyController>().SpawnPoint = spawnPoint;
            }

            enemies.AddRange(enemyBatch);
         }
      }

      private void Update()
      {
         
      }
   }
}
