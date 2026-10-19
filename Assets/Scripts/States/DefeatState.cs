public class DefeatState : IBattleState
{
    public void Enter(BattleManager state)
    {
        state.CurrentPlayer.ResetBattleState();
        state.CurrentPlayer.ResetSkillCooldowns();
        EventManager.Instance.Notification(EventType.OnPlayerDead, state);
        RankingUI.ShowGameOver();
    }

    public void Exit(BattleManager state) { }
}
