using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleField : BaseField
{
    public Transform _test;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Camera c = Camera.main;

            var touchPoint = Input.mousePosition;
            var ray = c.ScreenPointToRay(touchPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var pos = hit.point;
                pos = this.transform.InverseTransformPoint(pos);
                                
                if (pos.x > -_renderSize.x / 2f + _placementOffset.x)
                {
                    //_test.localPosition = pos;

                    pos.x = Mathf.Clamp(pos.x + _renderSize.x / 2f - _placementOffset.x, 0f, _renderSize.x);
                    pos.y = Mathf.Clamp(_renderSize.y - (pos.y + _renderSize.y / 2f) - _placementOffset.y, 0f, _renderSize.y);
                    int x = Mathf.Clamp((int)pos.x / (int)_cellSize.x, 0, _size.x - 1);
                    int y = Mathf.Clamp((int)pos.y / (int)_cellSize.y, 0, _size.y - 1);
                    
                    print(y);

                    SetShipPosition(_test, x, y);
                }
            }
        }
    }

}
