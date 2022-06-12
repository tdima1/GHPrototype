using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Entities
{
   public interface IEntitySpawnerService
   {
      Transform SpawnEntity(Transform spawnPoint, Vector3 spawnPosition, int wanderingDistance);
      List<Transform> SpawnEntitiesAroundSource(Transform parent, int numberOfSpawns, int distanceFromSource);
      void DespawnEntity(Transform entity);
   }
}
