using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public const float ENEMY_DELAY = 0.5f;

    public enum StepOrders
    {
        None,
        Player,
        Enemy
    }

    public FleetPlacement PlayerPlacement { get; set; }
    public FleetPlacement EnemyPlacement { get; set; }
    public PlayerBattleField PlayerField { get; set; }
    public EnemyBattleField EnemyField { get; set; }
    public ShipsInventory Inventory { get; set; }

    public StepOrders Step { get; set; }

    #region Game Session
    public void StartSession()
    {
        this.Step = StepOrders.None;

        this.PlayerPlacement = CheckFleetPlacement(this.PlayerPlacement, this.PlayerField, this.Inventory);
        this.PlayerField.Clear();
        this.PlayerField.Fill(this.PlayerPlacement);

        this.EnemyPlacement = CheckFleetPlacement(this.EnemyPlacement, this.EnemyField, this.Inventory);
        this.EnemyField.Clear();
        this.EnemyField.Fill(this.EnemyPlacement);

        StartCoroutine(SessionProcess());
    }

    private IEnumerator SessionProcess()
    {
        this.Step = Random.Range(0, 2) == 0 ? StepOrders.Player : StepOrders.Enemy;

        if (this.Step == StepOrders.Enemy)
            yield return new WaitForSeconds(ENEMY_DELAY);

        while (true)
        {
            switch (this.Step)
            {
                case StepOrders.None:
                    break;

                case StepOrders.Player:
                    break;

                case StepOrders.Enemy:
                    yield return new WaitForSeconds(ENEMY_DELAY);
                    int x = Random.Range(0, this.PlayerField.Size.x);
                    int y = Random.Range(0, this.PlayerField.Size.y);

                    this.PlayerField.SetShot(x, y);
                    
                    if (this.PlayerField.FieldFilling[x, y] != FillTypes.Ship)
                        this.Step = StepOrders.Player;

                    break;
            }

            yield return null;
        }
    }

    public void StopSession()
    {
        StopAllCoroutines();

        this.Step = StepOrders.None;
    }

    public void MakeStep(int x, int y)
    {
        this.EnemyField.SetShot(x, y);

        if (this.EnemyField.FieldFilling[x, y] != FillTypes.Ship)
            this.Step = StepOrders.Enemy;
    }
    #endregion

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