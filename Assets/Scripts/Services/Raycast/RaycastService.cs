using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Services.Raycast
{
   internal class RaycastService : IRaycastService
   {
      [Inject] private readonly PathfindingLayers pathfindingLayers;

      public GameObject GetClickedGameObject(Vector3 mousePosition, Camera camera)
      {
         var mouseRayFromScreenToWorld = camera.ScreenPointToRay(mousePosition);

         Physics.Raycast(mouseRayFromScreenToWorld, out RaycastHit hitInfo);

         if(hitInfo.transform != null) {
            var hitObject = GetUpperMostParent(hitInfo);

            return hitObject;
         }

         return null;
      }

      public Vector3 GetWorldPoint(Vector3 mousePosition, Camera camera)
      {
         var mouseWorldPosition = camera.ScreenPointToRay(mousePosition);

         Physics.Raycast(mouseWorldPosition, out RaycastHit hitInfo, pathfindingLayers.GroundLayer);

         var worldPoint = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);

         return worldPoint;
      }

      private GameObject GetUpperMostParent(RaycastHit hitInfo)
      {
         GameObject result = hitInfo.transform.gameObject;

         while(result.transform.parent != null) {
            result = result.transform.parent.gameObject;
         }

         return result;
      }
   }
}
