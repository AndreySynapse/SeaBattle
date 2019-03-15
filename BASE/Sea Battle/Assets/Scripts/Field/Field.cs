using UnityEngine;

public enum FillTypes
{
    Empty,
    Shot,
    Ship
}

public class Field : MonoBehaviour
{
    [SerializeField] private Vector2Int _fullSize;
    [SerializeField] private Vector2Int _actualSize;
    [SerializeField] private Vector2 _placementOffset;
    
    public FillTypes[,] FieldFilling { get; set; }

    public Vector2Int FullSize { get { return _fullSize; } private set { _fullSize = value; } }

    public Vector2Int ActualSize { get { return _actualSize; } private set { _actualSize = value; } }

    public Vector2 PlacementOffset { get { return _placementOffset; } private set { _placementOffset = value; } }

    public void Clear()
    {
        if (this.FieldFilling == null)
            this.FieldFilling = new FillTypes[_actualSize.x, _actualSize.y];
        
        for (int y = 0; y < _actualSize.y; y++)
            for (int x = 0; x < _actualSize.x; x++)
                this.FieldFilling[x, y] = FillTypes.Empty;
    }
}
