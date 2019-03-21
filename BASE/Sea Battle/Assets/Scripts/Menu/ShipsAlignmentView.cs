using UnityEngine;
using AEngine.Menu;

public class ShipsAlignmentView : MenuView
{
    [SerializeField] private Field _field;
    [SerializeField] private ShipsInventory _inventory;

    public void OnPlayButtonClick()
    {
        TransitionManager.MakeTransition("Sea Battle Classic", EMenu.GameView.ToString());
    }

    public void OnRandomazePlacementButtonClick()
    {
        _field.Clear();
        _field.Fill(_inventory);
    }

    public void OnBackButtonClick()
    {
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}
