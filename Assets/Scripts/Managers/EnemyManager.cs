using Assets.Scripts.Services.Entities;
using Assets.Scripts.Services.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
   public class EnemyManager : MonoBehaviour
   {
      [Inject] private readonly IEntitySpawnerService entitySpawnerService;
      [Inject] private readonly IMovementService movementService;

      [SerializeField] private Transform enemyPrefab;
      [SerializeField] private Transform enemyParent;
      [SerializeField] private Transform player;

      private List<Transform> enemies = new List<Transform>();

      private void Start()
      {
         enemies = entitySpawnerService.SpawnEntitiesAroundSource(player.position, enemyPrefab, enemyParent);
      }
   }
}
