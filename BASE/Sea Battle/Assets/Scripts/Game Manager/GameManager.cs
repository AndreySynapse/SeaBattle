using UnityEngine;
using AEngine;

public enum GameTypes
{
    Classic,
    Special
}

public class GameManager : MonoSingleton<GameManager>
{
    private BaseGameSession _gameSession;

    public BaseGameSession GameSession
    {
        get
        {
            if (_gameSession == null)
                CreateGameSession(GameTypes.Classic);

            return _gameSession;
        }

        private set { _gameSession = value; }
    }

    protected override void Init()
    {
        base.Init();

        DontDestroyOnLoad(this);
    }

    public void CreateGameSession(GameTypes gameType)
    {
        if (_gameSession != null)
            Destroy(_gameSession);

        switch (gameType)
        {
            case GameTypes.Classic:
                _gameSession = this.gameObject.AddComponent<ClassicGameSession>();
                break;
        }
    }
}