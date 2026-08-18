public class DefeatState : IBattleState
{
    public void Enter(BattleManager state)
    {
        EventManager.Instance.Notification(EventType.OnPlayerDead, state);
    }

    public void Exit(BattleManager state) { }
}
