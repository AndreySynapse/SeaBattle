using AEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameSession GameSession { get; set; }
    
    protected override void Init()
    {
        base.Init();

        if (this.GameSession == null)
            this.GameSession = this.gameObject.AddComponent<GameSession>();

        DontDestroyOnLoad(this);
    }
}