using AEngine.Menu;

public class GameView : MenuView
{
    private GameManager _manager;

    public override void OnShowMenu()
    {
        base.OnShowMenu();

        if (_manager == null)
            _manager = GameManager.Instance;
        
        _manager.GameSession.StartSession();
    }
    
    public void OnCloseButtonClick()
    {
        _manager.GameSession.StopSession();
        
        TransitionManager.MakeTransition("Menu", EMenu.MainMenu.ToString());
    }
}
