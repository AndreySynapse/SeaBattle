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

        foreach (var item in _inventory.Ships)
        {
            for (int i = 0; i < item.count; i++)
            {
                int length = item.ship.Length;

                
            }
        }
    }

    public void OnBackButtonClick()
    {
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}
