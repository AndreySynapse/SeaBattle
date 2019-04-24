using UnityEngine;

public class PlayerBattleField : Field
{
    [SerializeField] private Collider _cachedCollider;
    [SerializeField] private Transform _shot;

    private GameSession _session;
    private Placeholder _placeHolder;

    private void Start()
    {
        _session = GameManager.Instance.GameSession;
                
        _shot.SetAsLastSibling();
    }

    private void Update()
    {
        if (_session.Step == GameSession.StepOrders.Enemy)
        {
            int x = Random.Range(0, _logicSize.x);
            int y = Random.Range(0, _logicSize.y);

            SetShot(x, y);
            _session.Step = GameSession.StepOrders.Player;
        }
    }

    private void SetShot(int x, int y)
    {
        var shot = GameObject.Instantiate(_shot, Vector3.zero, Quaternion.identity, _cachedTransform) as Transform;
        shot.localScale = Vector3.one;

        PutObjectToField(shot, x, y);
        shot.gameObject.SetActive(true);
    }
}