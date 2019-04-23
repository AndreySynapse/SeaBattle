using AEngine;

public enum GameTypes
{
    Classic,
    Special
}

public class GameManager : MonoSingleton<GameManager>
{
    public GameSession GameSession { get; set; }
    
    protected override void Init()
    {
        base.Init();

        this.GameSession = this.gameObject.AddComponent<GameSession>();

        DontDestroyOnLoad(this);
    }
}