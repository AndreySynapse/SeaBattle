using System.Collections.Generic;
using UnityEngine;

public class EnemyStepLogic
{
    public enum StepStates
    {
        First,
        Correct,
        Error
    }

    private enum Directions
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    private List<Vector2Int> _targetShips;
    private Vector2Int _nextTargetPoint;
    private Directions _lastDirection;

    public StepStates StepState { get; set; }
        
    public EnemyStepLogic()
    {
        _targetShips = new List<Vector2Int>();
        this.StepState = StepStates.First;
    }

    public Vector2Int GetTargetPoint(PlayerBattleField field)
    {
        if (this.StepState == StepStates.First || _targetShips.Count == 0)
            return GetRandomPoint(field);
        else
            return GetShipTargetPoint(field);
    }

    private Vector2Int GetRandomPoint(PlayerBattleField field)
    {
        Vector2Int target = Vector2Int.zero;

        var space = field.GetFreeHorizontalSpace();

        if (space.Count > 0)
        {
            var line = space[Random.Range(0, space.Count)];

            target.x = line.list[Random.Range(0, line.list.Count)];
            target.y = line.index;

            if (field.FieldFilling[target.x, target.y] == FillTypes.Ship)
                _targetShips.Add(target);

            this.StepState = StepStates.Correct;
        }
        else
            this.StepState = StepStates.Error;

        return target;
    }

    private Vector2Int GetShipTargetPoint(PlayerBattleField field)
    {
        if (_targetShips.Count == 0)
        {
            Debug.LogError("Null target");
            return GetRandomPoint(field);
        }

        Vector2Int point = _targetShips[0];
        Ship targetShip = field.FindShip(point.x, point.y);

        if (targetShip == null)
        {
            _targetShips.RemoveAt(0);
            _lastDirection = Directions.None;
            return GetRandomPoint(field);
        }

        List<Directions> directions = new List<Directions>() { Directions.Left, Directions.Right, Directions.Up, Directions.Down };
        Directions dir = _lastDirection == Directions.None ? directions[Random.Range(0, directions.Count)] : _lastDirection;
        
        while (directions.Count > 0)
        {
            if (CheckDirection(field, dir, point, targetShip))
            {
                if (targetShip.Lives <= 1 && field.FieldFilling[_nextTargetPoint.x, _nextTargetPoint.y] == FillTypes.Ship)
                {
                    _targetShips.RemoveAt(0);
                    _lastDirection = Directions.None;
                }

                return _nextTargetPoint;
            }
            else
            {
                directions.Remove(dir);
                dir = directions.Count > 0 ? directions[Random.Range(0, directions.Count)] : dir;
            }
        }
        
        return GetRandomPoint(field);
    }

    private bool CheckDirection(PlayerBattleField field, Directions direction, Vector2Int point, Ship ship)
    {
        switch (direction)
        {
            case Directions.Left:
                for (int i = 1; i <= ship.Size.x && point.x - i >= 0; i++)
                {
                    var pos = field.FieldFilling[point.x - i, point.y];
                    if (pos == FillTypes.Ship || pos == FillTypes.Empty || pos == FillTypes.NearShip)
                    {
                        _nextTargetPoint = new Vector2Int(point.x - i, point.y);
                        _lastDirection = Directions.Left;
                        return true;
                    }
                    else if (pos == FillTypes.Shot)
                        break;
                }
                break;

            case Directions.Right:
                for (int i = 1; i <= ship.Size.x && point.x + i < field.Size.x; i++)
                {
                    var pos = field.FieldFilling[point.x + i, point.y];
                    if (pos == FillTypes.Ship || pos == FillTypes.Empty || pos == FillTypes.NearShip)
                    {
                        _nextTargetPoint = new Vector2Int(point.x + i, point.y);
                        _lastDirection = Directions.Right;
                        return true;
                    }
                    else if (pos == FillTypes.Shot)
                        break;
                }
                break;

            case Directions.Up:
                for (int i = 1; i <= ship.Size.y && point.y - i >= 0; i++)
                {
                    var pos = field.FieldFilling[point.x, point.y - i];
                    if (pos == FillTypes.Ship || pos == FillTypes.Empty || pos == FillTypes.NearShip)
                    {
                        _nextTargetPoint = new Vector2Int(point.x, point.y - i);
                        _lastDirection = Directions.Up;
                        return true;
                    }
                    else if (pos == FillTypes.Shot)
                        break;
                }
                break;

            case Directions.Down:
                for (int i = 1; i <= ship.Size.y && point.y + i < field.Size.y; i++)
                {
                    var pos = field.FieldFilling[point.x, point.y + i];
                    if (pos == FillTypes.Ship || pos == FillTypes.Empty || pos == FillTypes.NearShip)
                    {
                        _nextTargetPoint = new Vector2Int(point.x, point.y + i);
                        _lastDirection = Directions.Down;
                        return true;
                    }
                    else if (pos == FillTypes.Shot)
                        break;
                }
                break;
        }

        return false;
    }
}