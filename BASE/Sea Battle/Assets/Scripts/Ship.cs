using UnityEngine;

public enum ShipOrientations
{
    Horizontal,
    Vertical
}

public class Ship : MonoBehaviour
{
    [SerializeField] private Transform _cachedTransform;
    [SerializeField] private Vector2Int _size;

    public Transform CachedTransform { get { return _cachedTransform; } }
    public Vector2Int Size { get { return _size; } }
    public Vector2Int Position { get; set; }
    public int Lives { get; set; }

    public void Init()
    {
        this.Lives = this.Size.x * this.Size.y;
    }

    public bool IsShipSpace(int x, int y)
    {
        return (this.Position.x <= x && x < this.Position.x + _size.x) && (this.Position.y <= y && y < this.Position.y + _size.y);
    }

    public void SetDamage(int x, int y)
    {
        this.Lives--;
    }
}