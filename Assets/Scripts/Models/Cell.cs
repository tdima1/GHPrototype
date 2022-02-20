using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
   public class Cell
   {
      public Vector3Int GridPosition { get; set; }
      public Vector3 WorldPosition { get; set; }
   }
}
