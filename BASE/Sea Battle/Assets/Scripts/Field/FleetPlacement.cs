using System.Collections.Generic;
using UnityEngine;

public class FleetPlacement
{
    public class ShipPlacement
    {
        ShipTypes ShipType { get; set; }
        ShipOrientations Orientation { get; set; }
        Vector2 Position { get; set; }
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