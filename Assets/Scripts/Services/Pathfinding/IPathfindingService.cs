using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Pathfinding
{
   public interface IPathfindingService
   {
      List<Vector3> GetPath(Vector3Int position, Vector3Int destination, Grid<Cell> movementGrid, bool useDiagonals = false);
   }
}
