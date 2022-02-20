using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Movement
{
   public interface IMovementService
   {
      void MoveUnit(Transform unit,Vector3 destination);
   }
}
