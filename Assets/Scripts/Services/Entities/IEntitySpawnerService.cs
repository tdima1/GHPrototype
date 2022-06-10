using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services.Entities
{
   public interface IEntitySpawnerService
   {
      Transform SpawnEntity(Transform entity, Transform parent, Vector3 spawnPosition);
      List<Transform> SpawnEntitiesAroundSource(Vector3 sourcePosition, Transform entity, Transform parent);
      void DespawnEntity(Transform entity);
   }
}
