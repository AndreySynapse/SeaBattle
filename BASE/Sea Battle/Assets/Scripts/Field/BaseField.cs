using UnityEngine;

public enum FillTypes
{
    Empty,
    Shot,
    Ship
}

public class BaseField : MonoBehaviour
{
    [SerializeField] protected Vector2Int _renderSize;
    [SerializeField] protected Vector2Int _size;
    [SerializeField] protected Vector2 _placementOffset;

    protected Vector2 _cellSize;
    protected Vector2 _startPosition;

    public FillTypes[,] FieldFilling { get; set; }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _cellSize = new Vector2((_renderSize.x - _placementOffset.x) / _size.x, (_renderSize.y - _placementOffset.y) / _size.y);
        _startPosition = new Vector2((-_renderSize.x / 2f) + _placementOffset.x + _cellSize.x / 2f, (_renderSize.y / 2f) - _placementOffset.y - _cellSize.y / 2f);

        this.FieldFilling = new FillTypes[_size.x, _size.y];
        ClearFieldFilling();
    }

    protected void ClearFieldFilling()
    {
        for (int y = 0; y < _size.y; y++)
            for (int x = 0; x < _size.x; x++)
                this.FieldFilling[x, y] = FillTypes.Empty;
    }

    protected void SetShipPosition(Transform ship, int x, int y)
    {
        ship.localPosition = _startPosition + Vector2.right * x * _cellSize.x + Vector2.down * y * _cellSize.y;
    }
}
