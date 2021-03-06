﻿using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Ship Data", order = 50)]
public class ShipData : ScriptableObject
{
    [SerializeField] private int _length;
    [SerializeField] private GameObject _horizontalPrefab;
    [SerializeField] private GameObject _verticalPrefab;
        
    public int Length { get { return _length; } }

    public GameObject HorizontalPrefab { get { return _horizontalPrefab; } }

    public GameObject VerticalPrefab { get { return _verticalPrefab; } }
}
