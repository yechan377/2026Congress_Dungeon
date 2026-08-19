using UnityEngine;

public class PlayerTurnState : IBattleState
{
    public void Enter(BattleManager state)
    {

    }

    public void Exit(BattleManager state) 
    {
        BattleManager.Instance.OnPlayerAction();
    }
}
