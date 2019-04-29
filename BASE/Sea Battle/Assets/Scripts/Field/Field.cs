using System.Collections.Generic;
using UnityEngine;

public enum FillTypes
{
    Empty,
    Shot,
    Ship,
    NearShip
}

public class Field : MonoBehaviour
{
    [SerializeField] protected Transform _cachedTransform;
    [SerializeField] protected Vector2Int _renderSize;
    [SerializeField] protected Vector2Int _logicSize;
    [SerializeField] protected Vector2 _placementOffset;

    protected Vector2 _cellSize;
    protected Vector2 _startPosition;
    private List<GameObject> _fleet;

    public FillTypes[,] FieldFilling { get; set; }
    public Vector2Int Size { get { return _logicSize; } }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _fleet = new List<GameObject>();

        _cellSize = new Vector2((_renderSize.x - _placementOffset.x) / _logicSize.x, (_renderSize.y - _placementOffset.y) / _logicSize.y);
        _startPosition = new Vector2((-_renderSize.x / 2f) + _placementOffset.x + _cellSize.x / 2f, (_renderSize.y / 2f) - _placementOffset.y - _cellSize.y / 2f);

        this.FieldFilling = new FillTypes[_logicSize.x, _logicSize.y];
        Clear();
    }

    public void Clear()
    {
        for (int y = 0; y < _logicSize.y; y++)
            for (int x = 0; x < _logicSize.x; x++)
                this.FieldFilling[x, y] = FillTypes.Empty;

        if (_fleet != null && _fleet.Count > 0)
        {
            foreach (var item in _fleet)
                Destroy(item);

            _fleet.Clear();
        }
    }

    protected void PutObjectToField(Transform target, int x, int y)
    {
        target.localPosition = _startPosition + Vector2.right * x * _cellSize.x + Vector2.down * y * _cellSize.y;
    }

    public virtual void Fill(FleetPlacement fleet)
    {
        Clear();
        
        foreach (FleetPlacement.ShipPlacement shipPlacement in fleet.Placements)
        {
            GameObject prefab = shipPlacement.Orientation == ShipOrientations.Horizontal ? shipPlacement.ShipData.HorizontalPrefab : shipPlacement.ShipData.VerticalPrefab;
            PutObjectToField(CreateShip(prefab), shipPlacement.Position.x, shipPlacement.Position.y);

            Ship ship = prefab.GetComponent<Ship>();
            for (int i = 0; i < ship.Size.x; i++)
                for (int j = 0; j < ship.Size.y; j++)
                    this.FieldFilling[shipPlacement.Position.x + i, shipPlacement.Position.y + j] = FillTypes.Ship;
        }
    }

    private Transform CreateShip(GameObject prefab)
    {
        Transform shipTransform = GameObject.Instantiate<Transform>(prefab.transform, Vector3.zero, Quaternion.identity, this.transform);
        shipTransform.localPosition = Vector3.zero;
        shipTransform.localScale = Vector3.one;

        _fleet.Add(shipTransform.gameObject);

        return shipTransform;
    }
}