﻿using System;
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
      [Inject] private readonly RaycastConstants raycastConstants;
      [Inject] private readonly CollisionConstants collisionConstants;

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

      public (bool ObjectHit, Vector3 GroundPosition) GetGroundPoint(Vector3 origin)
      {
         var originToInt = Vector3Int.RoundToInt(origin);
         Vector3 raySourcePosition = originToInt + Vector3.up * raycastConstants.RayHeight;
         var hit = Physics.Raycast(raySourcePosition, Vector3.down, out RaycastHit hitInfo, raycastConstants.RayLength, pathfindingLayers.GroundLayer);

         if(hit) {
            bool collision = Physics.CheckCapsule(hitInfo.point, hitInfo.point + Vector3.up * collisionConstants.Height, collisionConstants.Radius, pathfindingLayers.ObstacleLayer);

            if(collision) {
               hit = false;
            }
         }

         return (hit, hitInfo.point);
      }

      public (bool ObjectHit, Vector3 WorldPosition) GetWorldGroundPoint(Vector3 mousePosition, Camera camera)
      {
         var mouseRayFromScreenToWorld = camera.ScreenPointToRay(mousePosition);
         var hit = Physics.Raycast(mouseRayFromScreenToWorld, out RaycastHit hitInfo, raycastConstants.RayCameraToGroundLength, pathfindingLayers.GroundLayer);

         var worldPoint = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
         return (hit, worldPoint);
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
