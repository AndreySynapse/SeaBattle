using System;
using UnityEngine;
using AEngine;

public class Cell : MonoBehaviour
{
    public static event Action<Vector2Int> OnCellClickEvent;

    public Vector2Int Index { get; set; }

    public void OnCellClick()
    {
        OnCellClickEvent.SafeInvoke(this.Index);
    }
}
