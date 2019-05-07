using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AEngine;

public class GameSession : MonoBehaviour
{
    public const float ENEMY_DELAY = 0.25f;

    public enum StepOrders
    {
        None,
        Player,
        Enemy
    }

    public event Action OnGameOver;

    private EnemyStepLogic _botLogic;
    private bool _isActive;

    public FleetPlacement PlayerPlacement { get; set; }
    public FleetPlacement EnemyPlacement { get; set; }
    public PlayerBattleField PlayerField { get; set; }
    public EnemyBattleField EnemyField { get; set; }
    public ShipsInventory Inventory { get; set; }

    public StepOrders Step { get; set; }

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

            _botLogic = new EnemyStepLogic();

            this.Step = UnityEngine.Random.Range(0, 2) == 0 ? StepOrders.Player : StepOrders.Enemy;
            
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
                                        
                    Vector2Int target = _botLogic.GetTargetPoint(this.PlayerField);

                    if (_botLogic.StepState == EnemyStepLogic.StepStates.Correct)
                        MakeStep(this.PlayerField, target.x, target.y);
                    else
                        this.Step = StepOrders.None;

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
            
            if (field.FieldFilling[x, y] == FillTypes.WreckedShip)
            {
                Ship ship = FindShip(field.Fleet, x, y);

                if (ship != null)
                {
                    ship.SetDamage(x, y);
                    
                    if (ship.Lives <= 0)
                        PutShipToPool(field, ship);
                    if (field.Fleet.Count <= 0)
                    {
                        this.Step = StepOrders.None;
                        OnGameOver.SafeInvoke();
                    }
                }
                else
                    Debug.LogError("This ship should not be NULL");
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

    private void PutShipToPool(BattleField field, Ship ship)
    {
        for (int i = 0; i < field.Fleet.Count; i++)
            if (field.Fleet[i] == ship)
            {
                field.FleetPool.Add(field.Fleet[i]);
                field.Fleet.RemoveAt(i);
                break;
            }
    }
}