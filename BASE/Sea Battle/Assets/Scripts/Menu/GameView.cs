using UnityEngine;
using AEngine.Menu;

public class GameView : MenuView
{
    [SerializeField] private PlayerBattleField _playerField;
    [SerializeField] private EnemyBattleField _enemyField;
    [SerializeField] private ShipsInventory _inventory;
    [SerializeField] private GameObject _gameOverText;

    private GameSession _session;

    public override void OnShowMenu()
    {
        if (Application.isPlaying)
        {
            base.OnShowMenu();

            if (_session == null)
                _session = GameManager.Instance.GameSession;

            _session.PlayerField = _playerField;
            _session.EnemyField = _enemyField;
            _session.Inventory = _inventory;

            _session.OnGameOver -= OnGameOver;
            _session.OnGameOver += OnGameOver;

            _session.StartSession();
        }
    }
    
    public void OnCloseButtonClick()
    {
        _session.StopSession();
        _session.OnGameOver -= OnGameOver;

        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }

    #region Events
    private void OnGameOver()
    {
        _gameOverText.SetActive(true);
    }
    #endregion
}