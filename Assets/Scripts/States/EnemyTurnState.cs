public class EnemyTurnState : IBattleState
{
    public void Enter(BattleManager state)
    {
        foreach (var enemy in state.EnemySlot)
        {
            if (enemy.IsDead()) continue;
            state.CurrentPlayer.TakeDamage(10);

            if (state.CurrentPlayer.IsDead()) break;
        }

        state.CheckBattleEnd();
    }

    public void Exit(BattleManager state)
    {
        BattleManager.Instance.OnEnemyAction();
    }
}