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

        _session.PlayerField = _playerField;
        _session.EnemyField = _enemyField;
        _session.Inventory = _inventory;
        
        _session.StartSession();
    }
    
    public void OnCloseButtonClick()
    {
        _session.StopSession();
        
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}