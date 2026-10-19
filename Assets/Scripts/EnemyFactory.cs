using UnityEngine;

public static class EnemyFactory
{
    public static EnemyData Create(EnemyStat stat, int level, int stage = 1)
    {
        EnemyData enemy = new EnemyData();
        enemy.SetStats(level, stat);
        enemy.StageIndex = stage;

        if (stat.isBoss)
        {
            switch (stage)
            {
                case 1: enemy.Skills.Add(new SmashSkill()); break;
                case 2: enemy.Skills.Add(new ConfuseStrikeSkill()); enemy.Skills.Add(new AllOutStrikeSkill()); break;
                default: enemy.Skills.Add(new DarknessSkill()); enemy.Skills.Add(new SoulDrainSkill()); enemy.Skills.Add(new DestroyBeamSkill()); break;
            }
        }
        else if (stat.isElite)
        {
            enemy.Skills.Add(new EliteStrikeSkill());
        }
        return enemy;
    }
}
