using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public const float ENEMY_DELAY = 0.25f;

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

    private Placeholder placeholder;

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

        placeholder = new Placeholder();
        placeholder.Init(this.PlayerField);

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

                    /*
                    var list = placeholder.GetFreeHorizontalCells();
                    var subList = list[Random.Range(0, list.Count)];

                    int y = subList.index;
                    int x = subList.lists[Random.Range(0, subList.lists.Count)][0];
                    */

                    int x = Random.Range(0, this.PlayerField.Size.x);
                    int y = Random.Range(0, this.PlayerField.Size.y);
                                        
                    MakeStep(this.PlayerField, x, y);
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
        
    public void MakeStep(BattleField field, int x, int y)
    {
        if (field.FieldFilling[x, y] != FillTypes.WreckedShip && field.FieldFilling[x, y] != FillTypes.Shot)
        {
            field.SetShot(x, y);

            if (field.FieldFilling[x, y] != FillTypes.WreckedShip)
                this.Step = this.Step == StepOrders.Player ? StepOrders.Enemy : StepOrders.Player;
        }
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