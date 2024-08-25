using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CellConfig
    {
        public readonly List<CellConfig> NearbyCellsConfig = new();
        public int NumberCell { get; set; }
        public Vector2Int CellCoordinateInArray { get; set; }
    }
}
