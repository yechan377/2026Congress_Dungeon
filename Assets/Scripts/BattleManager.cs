using UnityEngine;

public class BattleManager : SingletonManagers<BattleManager>
{
    private PlayerData currentPlayer;
    private EnemyData currentEnemy;

    public void StartBattle(PlayerData player, EnemyData enemy)
    {
        currentPlayer = player;
        currentEnemy = enemy;
        Debug.Log($"전투 시작! 플레이어 HP: {player.HP}, 적: {enemy.HP}");
    }

    public void CheckDeath(Character character)
    {
        if (character is PlayerData playerData) PlayerDead();
        if (character is EnemyData enemyData) EnemyDead(enemyData);
    }

    public void PlayerDead()
    {
        EventManager.Instance.Notification(EventType.OnPlayerDead, this);
    }

    public void EnemyDead(EnemyData enemyData)
    {
        EventManager.Instance.Notification(EventType.OnEnemyDead, this, enemyData);
    }
}