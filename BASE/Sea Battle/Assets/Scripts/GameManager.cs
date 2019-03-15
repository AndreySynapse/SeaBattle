using UnityEngine;
using AEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public BaseGameSession GameSession;

    protected override void Init()
    {
        base.Init();
    }
}
