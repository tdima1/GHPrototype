using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Raycast
{
   public interface IRaycastService
   {
      GameObject GetClickedGameObject(Vector3 mousePosition, Camera camera);

      Vector3 GetWorldPoint(Vector3 mousePosition, Camera camera);

      (bool ObjectHit, Vector3 WorldPosition) GetGroundPoint(Vector3 origin);
   }
}
