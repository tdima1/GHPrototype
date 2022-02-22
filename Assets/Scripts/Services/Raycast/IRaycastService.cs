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

      (bool ObjectHit, Vector3 GroundPosition) GetGroundPoint(Vector3 origin);

      (bool ObjectHit, Vector3 WorldPosition) GetWorldPoint(Vector3 mousePosition, Camera camera);
   }
}
