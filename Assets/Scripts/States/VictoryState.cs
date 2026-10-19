using UnityEngine;

public class VictoryState : IBattleState
{
    public void Enter(BattleManager state)
    {
        foreach (var enemy in state.EnemySlot)
        {
            EventManager.Instance.Notification(EventType.OnEnemyDead, state, enemy);
        }

        state.CurrentPlayer.ResetBattleState();
        state.CurrentPlayer.ResetSkillCooldowns();

        GameObject.FindFirstObjectByType<BattleSceneManager>().ClearEnemies();

        if (SkillConfirmUI.Pending)
        {
            SkillConfirmUI.Get().SetOnComplete(GameManager.Instance.OnBattleVictory);
        }
        else
        {
            GameManager.Instance.OnBattleVictory();
        }
    }

    public void Exit(BattleManager state) { }
}