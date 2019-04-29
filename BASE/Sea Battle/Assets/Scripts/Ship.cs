using UnityEngine;

public enum ShipOrientations
{
    Horizontal,
    Vertical
}

public class Ship : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    public Vector2Int Size { get { return _size; } }
}
