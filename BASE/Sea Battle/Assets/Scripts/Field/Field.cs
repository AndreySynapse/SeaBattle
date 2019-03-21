using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FillTypes
{
    Empty,
    Shot,
    Ship
}

public class Field : MonoBehaviour
{
    private struct ListsLine
    {
        public int index;
        public List<List<int>> lists;
    }

    [SerializeField] private Vector2Int _fullSize;
    [SerializeField] private Vector2Int _actualSize;
    [SerializeField] private Vector2 _placementOffset;
    [SerializeField] private ShipsInventory _inventory;

    private Vector2 _startPosition;
    private Vector2 _cellSize;
    private FleetPlacement _fleet;
    
    public FillTypes[,] FieldFilling { get; set; }

    public Vector2Int FullSize { get { return _fullSize; } private set { _fullSize = value; } }

    public Vector2Int ActualSize { get { return _actualSize; } private set { _actualSize = value; } }

    public Vector2 PlacementOffset { get { return _placementOffset; } private set { _placementOffset = value; } }

    private FieldPlaceholder _fieldPlaceholder;

    private void Awake()
    {
        _cellSize = new Vector2((_fullSize.x - _placementOffset.x) / _actualSize.x, (_fullSize.y - _placementOffset.y) / _actualSize.y);
        _startPosition = new Vector2((-_fullSize.x / 2f) + _placementOffset.x + _cellSize.x / 2f, (_fullSize.y / 2f) - _placementOffset.y - _cellSize.y / 2f);

        _fleet = new FleetPlacement();

        this.FieldFilling = new FillTypes[_actualSize.x, _actualSize.y];
        
        _fieldPlaceholder = new FieldPlaceholder(this.FieldFilling, _actualSize.x, _actualSize.y);
    }
    
    public void Clear()
    {
        _fleet.Clear();

        for (int y = 0; y < _actualSize.y; y++)
            for (int x = 0; x < _actualSize.x; x++)
                this.FieldFilling[x, y] = FillTypes.Empty;
    }



    public void Fill(ShipsInventory inventory)
    {
        _fieldPlaceholder.Fill(_fleet, inventory);
                
        foreach (var ship in _fleet.Placements)
        {
            SetShipPosition(CreateShip(ship.Orientation == ShipOrientations.Horizontal ? ship.ShipData.HorizontalPrefab : ship.ShipData.VerticalPrefab), ship.Position.x, ship.Position.y);
        }
    }

    private Transform CreateShip(GameObject prefab)
    {
        Transform ship = GameObject.Instantiate<Transform>(prefab.transform, Vector3.zero, Quaternion.identity, this.transform);
        ship.localPosition = Vector3.zero;
        ship.localScale = Vector3.one;

        return ship;
    }

    private void SetShipPosition(Transform shipTransform, int x, int y)
    {
        shipTransform.localPosition = _startPosition + Vector2.right * x * _cellSize.x + Vector2.down * y * _cellSize.y;
    }
            
    private Vector2 PointToIndex(Vector2 position)
    {
        return new Vector2(((int)position.x) / ((int)_cellSize.x), ((int)position.y) / ((int)_cellSize.y));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera c = Camera.main;
            var ray = c.ScreenPointToRay(Input.mousePosition);

            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                var p = hit.point;
                p = transform.InverseTransformPoint(p);
                p = PointToIndex(p);
                print(p);
            }
        }
    }
}
