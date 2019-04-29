using UnityEngine;

public class PlayerBattleField : Field
{
    [SerializeField] private Collider _cachedCollider;
    [SerializeField] private Transform _shot;

    private GameSession _session;

    private void Start()
    {
        _session = GameManager.Instance.GameSession;
                
        _shot.SetAsLastSibling();
    }
    
    public void SetShot(int x, int y)
    {
        var shot = GameObject.Instantiate(_shot, Vector3.zero, Quaternion.identity, _cachedTransform) as Transform;
        shot.localScale = Vector3.one;

        PutObjectToField(shot, x, y);
        shot.gameObject.SetActive(true);
    }
}