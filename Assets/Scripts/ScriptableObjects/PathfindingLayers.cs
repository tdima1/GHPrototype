using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathfindingLayers", menuName = "ScriptableObjects/PathfindingLayers", order = 2)]
public class PathfindingLayers : ScriptableObject
{
   public LayerMask GroundLayer;
   public LayerMask EntityLayer;
   public LayerMask ObstacleLayer;
}
