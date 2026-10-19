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
        foreach (var e in EnemySlot) e.PlanAction();
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
        if (CurrentPlayer.SealTurns > 0) return;
        if (!skill.CanUse(CurrentPlayer)) return;

        if (!skill.RequiresTarget)
        {
            UseSkillOnTarget(skill, CurrentPlayer);
            return;
        }

        List<EnemyData> aliveEnemies = GetAliveEnemies();
        if (aliveEnemies.Count == 0) return;

        if (aliveEnemies.Count > 1)
        {
            FindFirstObjectByType<TargetSelectUI>().Show(aliveEnemies, skill);
            return;
        }

        UseSkillOnTarget(skill, aliveEnemies[0]);
    }

    public void UseSkillOnTarget(PlayerSkill skill, Character target)
    {
        skill.Execute(CurrentPlayer, target);
        CurrentPlayer.UseMp(skill.MpCost);
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

    public List<EnemyData> GetAliveEnemies()
    {
        List<EnemyData> result = new();
        foreach (var enemy in EnemySlot)
        {
            if (!enemy.IsDead()) result.Add(enemy);
        }
        return result;
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

    public void OnPlayerPassTurn()
    {
        if (!(currentState is PlayerTurnState)) return;

        CheckBattleEnd();
    }

    public void CheckBattleEnd()
    {
        if (AllEnemiesDead()) { CurrentPlayer.FillMp(CurrentPlayer.MaxMP); ChangeState(victory); return; }
        if (CurrentPlayer.IsDead()) { ChangeState(defeat); return; }
        if (currentState is EnemyTurnState) { ChangeState(playerTurn); }
        else
        {
            if (twoAction) { ChangeState(playerTurn); twoAction = false; return; }
            else ChangeState(enemyTurn);
        }

        if (playerAction && enemyAction)
        {
            CurrentPlayer.OnTurnEnd();
            playerAction = false;
            enemyAction = false;
        }
    }
}