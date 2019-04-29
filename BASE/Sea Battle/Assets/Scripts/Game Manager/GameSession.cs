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
    public PlayerBattleField PlayerField { get; set; }
    public EnemyBattleField EnemyField { get; set; }
    public ShipsInventory Inventory { get; set; }

    public StepOrders Step { get; set; }

    public virtual void StartSession()
    {
        this.PlayerPlacement = CheckFleetPlacement(this.PlayerPlacement, this.PlayerField, this.Inventory);
        this.PlayerField.Clear();
        this.PlayerField.Fill(this.PlayerPlacement);

        this.EnemyPlacement = CheckFleetPlacement(this.EnemyPlacement, this.EnemyField, this.Inventory);
        this.EnemyField.Clear();
        this.EnemyField.Fill(this.EnemyPlacement);

        Step = Random.Range(0, 2) == 0 ? StepOrders.Player : StepOrders.Enemy;
    }

    public virtual void StopSession()
    {

    }

    private FleetPlacement CheckFleetPlacement(FleetPlacement fleet, Field field, ShipsInventory inventory)
    {
        if (fleet == null || fleet.Placements == null || fleet.Placements.Count == 0)
        {
            Placeholder holder = new Placeholder();
            fleet = holder.GeneratePlacement(field, inventory);
        }

        return fleet;
    }
}