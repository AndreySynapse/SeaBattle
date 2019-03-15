using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ships Inventory", menuName = "Ships Inventory", order = 51)]
public class ShipsInventory : ScriptableObject
{
    [Serializable]
    public class ShipsCell
    {
        public ShipData ship;
        public int count;
    }

    [SerializeField] private ShipsCell[] _ships;

    public ShipsCell[] Ships { get { return _ships; } }
    
}
