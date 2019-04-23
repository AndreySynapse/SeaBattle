using System.Collections.Generic;
using UnityEngine;

public class PlacementField : BaseField
{
    private struct ListsLine
    {
        public int index;
        public List<List<int>> lists;
    }

    [SerializeField] private ShipsInventory _inventory;
        
    private List<GameObject> _fleet;

    private Placeholder _fieldPlaceholder;

    public FleetPlacement PlayerPlacement { get; set; }
    public FleetPlacement EnemyPlacement { get; set; }

    protected override void Init()
    {
        base.Init();
                
        this.PlayerPlacement = new FleetPlacement();
        this.EnemyPlacement = new FleetPlacement();
        _fleet = new List<GameObject>();

        _fieldPlaceholder = new Placeholder(this.FieldFilling, _size.x, _size.y);
    }

    public void Clear()
    {
        this.PlayerPlacement.Clear();
        this.EnemyPlacement.Clear();

        foreach (var item in _fleet)
            Destroy(item);

        _fleet.Clear();

        for (int y = 0; y < _size.y; y++)
            for (int x = 0; x < _size.x; x++)
                this.FieldFilling[x, y] = FillTypes.Empty;
    }

    public void Fill(ShipsInventory inventory)
    {
        _fieldPlaceholder.Fill(this.EnemyPlacement, inventory);

        ClearFieldFilling();

        _fieldPlaceholder.Fill(this.PlayerPlacement, inventory);
                
        foreach (var ship in this.PlayerPlacement.Placements)
        {
            PutObjectToField(CreateShip(ship.Orientation == ShipOrientations.Horizontal ? ship.ShipData.HorizontalPrefab : ship.ShipData.VerticalPrefab), ship.Position.x, ship.Position.y);
        }
    }

    private Transform CreateShip(GameObject prefab)
    {
        Transform ship = GameObject.Instantiate<Transform>(prefab.transform, Vector3.zero, Quaternion.identity, this.transform);
        ship.localPosition = Vector3.zero;
        ship.localScale = Vector3.one;

        _fleet.Add(ship.gameObject);

        return ship;
    }
}
