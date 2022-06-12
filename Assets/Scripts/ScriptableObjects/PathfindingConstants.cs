using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PathfindingConstants", menuName = "ScriptableObjects/PathfindingConstants", order = 2)]
public class PathfindingConstants : ScriptableObject
{
   [Range(0, 3)] public float NeighbourHeightTolerance;
}
