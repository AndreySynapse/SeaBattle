using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Ship Data", order = 50)]
public class ShipData : ScriptableObject
{
    [SerializeField] private ShipTypes _type;
    [SerializeField] private int _length;
    [SerializeField] private GameObject _shipPrefab;

    public ShipTypes ShipType { get { return _type; } }

    public int Length { get { return _length; } }

    public GameObject ShipPrefab { get { return _shipPrefab; } }
}
