using UnityEngine;

public class BattleManager : SingletonManagers<BattleManager>
{
    public PlayerData CurrentPlayer { get; private set; }
    public EnemyData CurrentEnemy { get; private set; }
    private IBattleState currentState;
    private IBattleState playerTurn = new PlayerTurnState();
    private IBattleState enemyTurn = new EnemyTurnState();
    private IBattleState victory = new VictoryState();
    private IBattleState defeat = new DefeatState();

    public void StartBattle(PlayerData player, EnemyData enemy)
    {
        CurrentPlayer = player;
        CurrentEnemy = enemy;
        Debug.Log($"전투 시작 Player: {CurrentPlayer}, Enemy: {CurrentEnemy}");
        ChangeState(CurrentPlayer.Speed > CurrentEnemy.Speed ? playerTurn : enemyTurn);
    }

    public void ChangeState(IBattleState state)
    {
        currentState?.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }

    public void Attack(Character target, int damage)
    {
        target.TakeDamage(damage);
        CheckBattleEnd();
    }

    public void OnPlayerSelectSkill(/*Skill skill*/)
    {
        if (!(currentState is PlayerTurnState)) return; // 자기 턴 아니면 무시

        //int damage = skill.CalculateDamage(currentPlayer);
        //currentEnemy.TakeDamage(damage);
        CheckBattleEnd();
    }

    public void CheckBattleEnd()
    {
        if (CurrentEnemy.IsDead()) { ChangeState(victory); return; }
        if (CurrentPlayer.IsDead()) { ChangeState(defeat); return; }

        // 턴 교대
        ChangeState(currentState is PlayerTurnState ? new EnemyTurnState() : new PlayerTurnState());
    }
}