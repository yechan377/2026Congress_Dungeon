using UnityEngine;

public class EnemyTurnState : IBattleState
{
    public void Enter(BattleManager state)
    {
        Debug.Log("Àû ÅÏ");
        int damage = state.CurrentEnemy.GetAttackDamage();
        state.CurrentPlayer.TakeDamage(damage);
        state.CheckBattleEnd();
    }

    public void Exit(BattleManager state) { }
}
