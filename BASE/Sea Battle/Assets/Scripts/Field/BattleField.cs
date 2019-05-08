using System.Collections.Generic;
using UnityEngine;

public class BattleField : Field
{
    [SerializeField] protected Collider _cachedCollider;
    [SerializeField] protected Transform _shot;

    public void SetShot(int x, int y)
    {
        var shot = GameObject.Instantiate(_shot, Vector3.zero, Quaternion.identity, _cachedTransform) as Transform;
        shot.localScale = Vector3.one;

        PutObjectToField(shot, x, y);
        shot.gameObject.SetActive(true);

        if (this.FieldFilling[x, y] == FillTypes.Ship)
            this.FieldFilling[x, y] = FillTypes.WreckedShip;
        else
            this.FieldFilling[x, y] = FillTypes.Shot;
    }

    public Ship FindShip(int x, int y)
    {
        Ship ship = null;

        foreach (Ship item in this.Fleet)
        {
            if (item.IsShipSpace(x, y))
            {
                ship = item;
                break;
            }
        }

        return ship;
    }

    public void PutShipToPool(Ship ship)
    {
        for (int i = 0; i < this.Fleet.Count; i++)
            if (this.Fleet[i] == ship)
            {
                this.FleetPool.Add(this.Fleet[i]);
                this.Fleet.RemoveAt(i);
                break;
            }
    }
}
