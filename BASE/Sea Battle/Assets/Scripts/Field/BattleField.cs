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
}
