using AEngine.Menu;

public class MainMenu : MenuView
{
    public void OnPlayButtonClick()
    {
        TransitionManager.MakeTransitionInActiveScene(EMenu.ShipsAlignment.ToString());
    }
}
