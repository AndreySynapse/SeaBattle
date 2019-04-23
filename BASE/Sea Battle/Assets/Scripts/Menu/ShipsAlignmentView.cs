using UnityEngine;
using AEngine.Menu;

public class ShipsAlignmentView : MenuView
{
    [SerializeField] private PlacementField _field;
    [SerializeField] private ShipsInventory _inventory;

    public override void OnShowMenu()
    {
        base.OnShowMenu();
    }

    public void OnPlayButtonClick()
    {
        TransitionManager.MakeTransition("Sea Battle Classic", EMenu.GameView.ToString());
    }

    public void OnRandomazePlacementButtonClick()
    {
        _field.Clear();
        _field.Fill(_inventory);

        GameSession session = GameManager.Instance.GameSession;
        session.PlayerPlacement = _field.PlayerPlacement;
        session.EnemyPlacement = _field.EnemyPlacement;
    }

    public void OnBackButtonClick()
    {
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}