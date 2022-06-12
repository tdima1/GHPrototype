using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Entities
{
   public interface IEntitySpawnerService
   {
      Transform SpawnEntity(Transform entity, Vector3 spawnPosition);
      List<Transform> SpawnEntitiesAroundSource(Vector3 sourcePosition, int numberOfSpawns, int distanceFromSource, Transform parent);
      void DespawnEntity(Transform entity);
   }
}
