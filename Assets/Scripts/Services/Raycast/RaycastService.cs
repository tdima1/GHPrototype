using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Raycast
{
   internal class RaycastService : IRaycastService
   {
      public GameObject GetClickedGameObject()
      {
         Debug.Log("get clicked game object");
         return null;
      }
   }
}
