using UnityEngine;

public class EnemyBattleField : BattleField
{
    private GameSession _session;

    protected override void Init()
    {
        base.Init();

        _session = GameManager.Instance.GameSession;
        _shot.SetAsLastSibling();
    }

    private void Update()
    {
        if (_session.Step == GameSession.StepOrders.Player && Input.GetMouseButtonUp(0))
        {
            Camera c = Camera.main;

            var touchPoint = Input.mousePosition;
            var ray = c.ScreenPointToRay(touchPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider == _cachedCollider)
            {
                var pos = hit.point;
                pos = _cachedTransform.InverseTransformPoint(pos);

                if (pos.x > -_renderSize.x / 2f + _placementOffset.x && pos.y < _renderSize.y / 2f - _placementOffset.y)
                {
                    pos.x = Mathf.Clamp(pos.x + _renderSize.x / 2f - _placementOffset.x, 0f, _renderSize.x);
                    pos.y = Mathf.Clamp(_renderSize.y - (pos.y + _renderSize.y / 2f) - _placementOffset.y, 0f, _renderSize.y);
                    int x = Mathf.Clamp((int)pos.x / (int)_cellSize.x, 0, _logicSize.x - 1);
                    int y = Mathf.Clamp((int)pos.y / (int)_cellSize.y, 0, _logicSize.y - 1);
                    
                    _session.MakeStep(this, x, y);
                }
            }
        }
    }
}