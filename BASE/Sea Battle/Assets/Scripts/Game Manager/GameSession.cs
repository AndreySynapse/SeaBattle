using System.Collections;
using System.Collections.Generic;
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

    private bool _isActive;
    
    #region Game Session
    public void StartSession()
    {
        if (!_isActive)
        {
            this.Step = StepOrders.None;

            this.PlayerPlacement = CheckFleetPlacement(this.PlayerPlacement, this.PlayerField, this.Inventory);
            this.PlayerField.Clear();
            this.PlayerField.Fill(this.PlayerPlacement);

            this.EnemyPlacement = CheckFleetPlacement(this.EnemyPlacement, this.EnemyField, this.Inventory);
            this.EnemyField.Clear();
            this.EnemyField.Fill(this.EnemyPlacement);

            this.Step = Random.Range(0, 2) == 0 ? StepOrders.Player : StepOrders.Enemy;


            StartCoroutine(SessionProcess());
            _isActive = true;
        }
    }

    private IEnumerator SessionProcess()
    {
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

                    var space = this.PlayerField.GetFreeHorizontalSpace();

                    if (space.Count > 0)
                    {
                        var line = space[Random.Range(0, space.Count)];

                        int x = line.list[Random.Range(0, line.list.Count)];
                        int y = line.index;

                        MakeStep(this.PlayerField, x, y);
                    }
                    else
                    {
                        this.Step = StepOrders.None;
                    }
                    break;
            }

            yield return null;
        }
    }

    public void StopSession()
    {
        StopAllCoroutines();
        _isActive = false;
        this.Step = StepOrders.None;
    }
        
    public void MakeStep(BattleField field, int x, int y)
    {
        if (field.FieldFilling[x, y] != FillTypes.WreckedShip && field.FieldFilling[x, y] != FillTypes.Shot)
        {
            field.SetShot(x, y);

            /// TODO: find and destroy ship if need. Use lives.
            if (field.FieldFilling[x, y] == FillTypes.WreckedShip)
            {
                Ship ship = FindShip(field.Fleet, x, y);
                
            }

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

    private Ship FindShip(List<Ship> fleet, int x, int y)
    {
        Ship ship = null;

        foreach (Ship item in fleet)
        {
            if (item.IsShipSpace(x, y))
            {
                ship = item;
                break;
            }
        }

        return ship;
    }
}