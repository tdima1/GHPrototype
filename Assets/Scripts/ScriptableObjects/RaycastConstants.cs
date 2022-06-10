using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaycastConstants", menuName = "ScriptableObjects/RaycastConstants", order = 2)]
public class RaycastConstants : ScriptableObject
{
   [Range(100, 500)] public int RayHeight;
   [Range(100, 500)] public int RayLength;
   [Range(100, 500)] public int RayCameraToGroundLength;
}
