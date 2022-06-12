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
      [SerializeField] private Transform enemyPrefab;
      [SerializeField] private List<Transform> spawnPoints;

      [SerializeField][Range(0,10)] private int initialSpawns;
      [SerializeField] [Range(1, 7)] private int initialDistanceFromSource;
      [SerializeField] [Range(1, 5)] private int maxEnemiesPerSpawn;

      private List<Transform> enemies = new List<Transform>();

      private float timeFromLastWandering = 0f;
      private float timeUntilNextWandering = 0f;

      private void Start()
      {
         var i = 0;
         foreach(var spawnPoint in spawnPoints) {
            i++;
            var enemiesa = entitySpawnerService.SpawnSingularEntitiesAroundSource(spawnPoint.position, initialSpawns, initialDistanceFromSource, enemyPrefab, enemyParent);

            foreach(var enemy in enemiesa) {
               enemy.GetComponent<EnemyController>().SpawnPoint = spawnPoint;
            }

            enemies.AddRange(enemiesa);
         }
      }

      private void Update()
      {
         
      }
   }
}
