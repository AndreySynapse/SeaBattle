using UnityEngine;

public class EnemyStepLogic
{
    public enum StepStates
    {
        Correct,
        Error
    }

    public StepStates StepState { get; set; }

    public Vector2Int GetTargetPoint(PlayerBattleField field)
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
}
