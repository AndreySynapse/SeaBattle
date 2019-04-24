using UnityEngine;
using AEngine.Menu;

public class GameView : MenuView
{
    [SerializeField] private PlayerBattleField _playerField;
    [SerializeField] private EnemyBattleField _enemyField;
    [SerializeField] private ShipsInventory _inventory;

    private GameSession _session;

    public override void OnShowMenu()
    {
        base.OnShowMenu();

        if (_session == null)
            _session = GameManager.Instance.GameSession;
                
        _session.PlayerPlacement = CheckFleetPlacement(_session.PlayerPlacement, _playerField);
        _playerField.Clear();
        _playerField.Fill(_session.PlayerPlacement);

        _session.EnemyPlacement = CheckFleetPlacement(_session.EnemyPlacement, _enemyField);
        _enemyField.Clear();
        _enemyField.Fill(_session.EnemyPlacement);

        _session.StartSession();
    }
    
    public void OnCloseButtonClick()
    {
        _session.StopSession();
        
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }

    private FleetPlacement CheckFleetPlacement(FleetPlacement fleet, Field field)
    {
        if (fleet == null || fleet.Placements == null || fleet.Placements.Count == 0)
        {
            Placeholder holder = new Placeholder();
            fleet = holder.GeneratePlacement(field, _inventory);
        }

        return fleet;
    }
}