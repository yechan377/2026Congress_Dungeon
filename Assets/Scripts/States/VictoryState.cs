using UnityEngine;

public class VictoryState : IBattleState
{
    public void Enter(BattleManager state)
    {
        EventManager.Instance.Notification(EventType.OnEnemyDead, state, state.CurrentEnemy);
    }

    public void Exit(BattleManager state) { }
}
