using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Directions", menuName = "ScriptableObjects/Directions", order = 2)]
public class Directions : ScriptableObject
{
   public Vector3 Up;
   public Vector3 Down;
   public Vector3 Left;
   public Vector3 Right;
}
