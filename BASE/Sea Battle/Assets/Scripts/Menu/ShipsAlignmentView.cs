using UnityEngine;
using AEngine.Menu;

public class ShipsAlignmentView : MenuView
{
    [SerializeField] private Field _field;
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
        Placeholder holder = new Placeholder();
        _field.Clear();

        GameSession session = GameManager.Instance.GameSession;
        session.PlayerPlacement = holder.GeneratePlacement(_field, _inventory);

        _field.Fill(session.PlayerPlacement);
    }

    public void OnBackButtonClick()
    {
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}