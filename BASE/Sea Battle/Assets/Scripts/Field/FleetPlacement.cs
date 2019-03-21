using System.Collections.Generic;
using UnityEngine;

public class FleetPlacement
{
    public class ShipPlacement
    {
        public ShipData ShipData { get; set; }
        public ShipOrientations Orientation { get; set; }
        public Vector2Int Position { get; set; }
    }

    public List<ShipPlacement> Placements { get; set; }

    public FleetPlacement()
    {
        this.Placements = new List<ShipPlacement>();
    }

    public void Clear()
    {
        this.Placements.Clear();
    }
}