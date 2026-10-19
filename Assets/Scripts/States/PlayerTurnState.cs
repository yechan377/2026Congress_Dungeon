using UnityEngine;

public class PlayerTurnState : IBattleState
{
    public void Enter(BattleManager state)
    {
        var p = state.CurrentPlayer;
        if (p.StunTurns > 0)
        {
            p.StunTurns--;
            Debug.Log("기절! 턴을 넘긴다");
            state.CheckBattleEnd();
        }
    }

    public void Exit(BattleManager state) 
    {
        BattleManager.Instance.OnPlayerAction();
    }
}
