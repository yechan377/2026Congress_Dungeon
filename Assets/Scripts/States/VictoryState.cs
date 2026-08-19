using UnityEngine;
public class VictoryState : IBattleState
{
    public void Enter(BattleManager state)
    {
        foreach (var enemy in state.EnemySlot)
        {
            EventManager.Instance.Notification(EventType.OnEnemyDead, state, enemy);
        }
        GameManager.Instance.Victory();
    }
    public void Exit(BattleManager state) { }
}