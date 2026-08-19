using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SingletonManagers<BattleManager>
{
    public PlayerData CurrentPlayer { get; private set; }
    public List<EnemyData> EnemySlot { get; private set; } = new();
    private IBattleState currentState;
    private IBattleState playerTurn = new PlayerTurnState();
    private IBattleState enemyTurn = new EnemyTurnState();
    private IBattleState victory = new VictoryState();
    private IBattleState defeat = new DefeatState();
    private bool playerAction = false;
    private bool enemyAction = false;
    private bool twoAction = false;

    public Action OnTurnChanged;

    public void StartBattle(PlayerData player,List<EnemyData> enemy)
    {
        CurrentPlayer = player;
        EnemySlot = enemy;
        ChangeState(playerTurn);
    }

    public void ChangeState(IBattleState state)
    {
        currentState?.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }

    public void OnPlayerUseSkill(PlayerSkill skill)
    {
        if (!(currentState is PlayerTurnState)) return;
        if (!skill.CanUse(CurrentPlayer))
        {
            Debug.Log("MP 부족 또는 쿨타임");
            return;
        }

        EnemyData target = GetFirstAliveEnemy();
        if (target == null) return;

        skill.Execute(CurrentPlayer, target);
        Debug.Log($"{skill}사용");
        CheckBattleEnd();
    }

    public bool AllEnemiesDead()
    {
        foreach (var enemy in EnemySlot)
        {
            if (!enemy.IsDead()) return false;
        }
        return true;
    }

    public EnemyData GetFirstAliveEnemy()
    {
        foreach (var enemy in EnemySlot)
        {
            if (!enemy.IsDead()) return enemy;
        }
        return null;
    }

    public void OnPlayerAction()
    {
        playerAction = true;
    }

    public void OnEnemyAction()
    {
        enemyAction = true;
    }

    public void OnTwoAction()
    {
        twoAction = true;
    }

    public void CheckBattleEnd()
    {
        if (AllEnemiesDead()) { ChangeState(victory); return; }
        if (CurrentPlayer.IsDead()) { ChangeState(defeat); return; }
        if (currentState is EnemyTurnState) { ChangeState(playerTurn); }
        else
        {
            if (twoAction) { ChangeState(playerTurn); twoAction = false; return; }
            else ChangeState(enemyTurn);
        }

        if (playerAction && enemyAction)
        {
            Debug.Log("턴 종료");
            CurrentPlayer.OnTurnEnd();
            playerAction = false;
            enemyAction = false;
        }
    }
}