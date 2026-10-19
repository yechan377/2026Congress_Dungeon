using UnityEngine;

public class EnemyTurnState : IBattleState
{
    public void Enter(BattleManager state)
    {
        PlayerData player = state.CurrentPlayer;

        foreach (var enemy in state.EnemySlot)
        {
            if (enemy.IsDead()) continue;

            enemy.CheckPhase(player);

            if (enemy.Intent == EnemyIntent.Skill && enemy.PlannedSkill != null)
            {
                enemy.PlannedSkill.Execute(enemy, player);
            }
            else if (enemy.Intent == EnemyIntent.Attack)
            {
                player.TakeDamage(enemy.GetDamage(1f));
            }
            // Guard / Stunned: 이번 턴 행동 없음

            if (enemy.StunTurns > 0 && enemy.Intent == EnemyIntent.Stunned) enemy.StunTurns--;

            foreach (var s in enemy.Skills) s.ReduceCooldown();
            enemy.ReduceBuffTurns();
            enemy.ReduceDeBuffTurns();

            if (player.IsDead()) break;
        }

        // 다음 턴 행동 예고
        foreach (var enemy in state.EnemySlot)
        {
            if (!enemy.IsDead()) enemy.PlanAction();
        }

        state.CheckBattleEnd();
    }

    public void Exit(BattleManager state)
    {
        BattleManager.Instance.OnEnemyAction();
    }
}
