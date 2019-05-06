public class PlayerBattleField : BattleField
{
    private GameSession _session;

    protected override void Init()
    {
        base.Init();

        _session = GameManager.Instance.GameSession;                
        _shot.SetAsLastSibling();
    }
}