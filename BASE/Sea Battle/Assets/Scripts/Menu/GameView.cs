using AEngine.Menu;

public class GameView : MenuView
{
    public override void OnShowMenu()
    {
        base.OnShowMenu();
    }

    public void OnCloseButtonClick()
    {
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}
