using UnityEngine;

public class GameSession : MonoBehaviour
{
    public enum StepOrders
    {
        Player,
        Enemy
    }

    public FleetPlacement PlayerPlacement { get; set; }
    public FleetPlacement EnemyPlacement { get; set; }

    public StepOrders Step { get; set; }

    public virtual void StartSession()
    {
        Step = Random.Range(0, 2) == 0 ? StepOrders.Player : StepOrders.Enemy;
    }

    public virtual void StopSession()
    {

    }
}