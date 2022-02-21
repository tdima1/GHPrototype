using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.Grid
{
   public interface IGridBuilderService
   {
      Grid<Cell> BuildGrid(Vector3Int origin);
   }
}
