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

        if (SkillConfirmUI.Instance.IsLevelUp)
        {
            SkillConfirmUI.Instance.SetOnComplete(GameManager.Instance.StoreSceneLoad);
        }
        else
        {
            GameManager.Instance.StoreSceneLoad();
        }
    }

    public void Exit(BattleManager state) { }
}