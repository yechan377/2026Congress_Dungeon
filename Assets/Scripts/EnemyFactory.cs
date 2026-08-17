using UnityEngine;

public static class EnemyFactory
{
    public static EnemyData Create(EnemyStat stat, int level)
    {
        EnemyData enemy = new EnemyData();
        enemy.SetStats(level, stat);
        return enemy;
    }
}