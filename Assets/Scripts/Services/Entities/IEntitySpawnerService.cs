using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Entities
{
   public interface IEntitySpawnerService
   {
      Transform SpawnEntity(Transform entity, Transform parent, Vector3 spawnPosition);
      List<Transform> SpawnSingularEntitiesAroundSource(Vector3 sourcePosition, int numberOfSpawns, int distanceFromSource, Transform entity, Transform parent);
      void DespawnEntity(Transform entity);
   }
}
