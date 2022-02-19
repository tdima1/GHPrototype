using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridBuilderData", menuName = "ScriptableObjects/GridBuilderData", order = 2)]
[Serializable]
public class GridBuilderData : ScriptableObject
{
   [Range(1, 50)]
   public int GridRadius = 10;
   [Range(1, 10)]
   public int GridCellSize;
}
