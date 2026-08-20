using UnityEngine;

public class VictoryState : IBattleState
{
    public void Enter(BattleManager state)
    {
        Debug.Log("½Â¸®");

        foreach (var enemy in state.EnemySlot)
        {
            EventManager.Instance.Notification(EventType.OnEnemyDead, state, enemy);
        }

        GameObject.FindFirstObjectByType<BattleSceneManager>().ClearEnemies();
        GameObject.FindFirstObjectByType<NextStageButton>().Show();
    }

    public void Exit(BattleManager state) { }
}