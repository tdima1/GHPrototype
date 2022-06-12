using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models.Spawning
{
   public class SpawnPoint
   {
      public Transform SpawnPointTransform;

      public int EntitiesSpawned;
      public List<Transform> Entities;

      public float TimeUntilNextSpawn;
   }
}
