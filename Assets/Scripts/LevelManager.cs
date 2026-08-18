using UnityEngine;

public class LevelManager : SingletonManagers<LevelManager>
{

    private void OnEnable()
    {
        EventManager.Instance.AddListener(EventType.OnEnemyDead, OnEnemyDead);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(EventType.OnEnemyDead, OnEnemyDead);
    }

    private void OnEnemyDead(EventType type, Component sender, params object[] args)
    {
        EnemyData enemy = (EnemyData)args[0];
        PlayerData player = BattleManager.Instance.CurrentPlayer;

        int expGain = enemy.ExpReward * enemy.Level;
        int goldGain = (enemy.ExpReward * enemy.Level) / 2;

        player.AddExp(expGain);
        player.AddGold(goldGain);

        Debug.Log($"°æÇèÄ¡ +{expGain}, °ñµå +{goldGain}");
    }
}