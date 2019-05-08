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

    private List<Vector2Int> _targetShips;

    public StepStates StepState { get; set; }
        
    public EnemyStepLogic()
    {
        _targetShips = new List<Vector2Int>();
        this.StepState = StepStates.First;
    }

    public Vector2Int GetTargetPoint(PlayerBattleField field)
    {
        return GetRandomPoint(field);
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

            this.StepState = StepStates.Correct;
        }
        else
            this.StepState = StepStates.Error;

        return target;
    }

    private Vector2Int GetShipTargetPoint(PlayerBattleField field)
    {
        Vector2Int target = Vector2Int.zero;

        return target;
    }
}
