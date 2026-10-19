using UnityEngine;

public class LevelManager : SingletonManagers<LevelManager>
{

    private void OnEnable()
    {
        EventManager.Instance.AddListener(EventType.OnEnemyDead, OnEnemyDead);
    }

    private void OnDisable()
    {
        if (EventManager.HasInstance) EventManager.Instance.RemoveListener(EventType.OnEnemyDead, OnEnemyDead);
    }

    private void OnEnemyDead(EventType type, Component sender, params object[] args)
    {
        EnemyData enemy = (EnemyData)args[0];
        PlayerData player = BattleManager.Instance.CurrentPlayer;

        int expGain = Mathf.Max(1, enemy.ExpReward * enemy.Level * 7 / 10);   // balance
        int goldGain = Mathf.Max(1, enemy.ExpReward * enemy.Level / 3);

        player.AddExp(expGain);
        player.AddGold(goldGain);

        Debug.Log($"°æÇèÄ¡ +{expGain}, °ñµå +{goldGain}");
    }
}