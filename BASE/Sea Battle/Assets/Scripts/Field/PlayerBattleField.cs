using UnityEngine;

public class PlayerBattleField : Field
{
    [SerializeField] private Transform _cachedTransform;
    [SerializeField] private Collider _cachedCollider;
    [SerializeField] private ShipsInventory _inventory;

    [SerializeField] private Transform _shot;

    private GameSession _session;
    private Placeholder _placeHolder;

    private void Start()
    {
        _session = GameManager.Instance.GameSession;

        Fill(_inventory);

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

    public void Fill(ShipsInventory inventory)
    {
        Clear();

        FleetPlacement fleet = _session.PlayerPlacement;

        if (fleet == null || fleet.Placements.Count == 0)
        {
            if (_placeHolder == null)
                _placeHolder = new Placeholder(this.FieldFilling, _logicSize.x, _logicSize.y);

            if (fleet == null)
                fleet = new FleetPlacement();

            _placeHolder.Fill(fleet, inventory);
        }

        foreach (var ship in fleet.Placements)
        {
            PutObjectToField(CreateShip(ship.Orientation == ShipOrientations.Horizontal ? ship.ShipData.HorizontalPrefab : ship.ShipData.VerticalPrefab), ship.Position.x, ship.Position.y);
        }
    }

    private Transform CreateShip(GameObject prefab)
    {
        Transform ship = GameObject.Instantiate<Transform>(prefab.transform, Vector3.zero, Quaternion.identity, this.transform);
        ship.localPosition = Vector3.zero;
        ship.localScale = Vector3.one;

        return ship;
    }

    private void SetShot(int x, int y)
    {
        var shot = GameObject.Instantiate(_shot, Vector3.zero, Quaternion.identity, _cachedTransform) as Transform;
        shot.localScale = Vector3.one;

        PutObjectToField(shot, x, y);
        shot.gameObject.SetActive(true);
    }
}
