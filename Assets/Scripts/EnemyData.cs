using UnityEngine;

public class EnemyData : Character
{
    public int expReward;
    public bool isBoss;

    public void SetStats(int level, EnemyStat stat)
    {
        this.Level = level;
        MaxHP = stat.baseHp + level * stat.hpPerLevel;
        HP = MaxHP;
        Damage = stat.baseDamage + level * stat.damagePerLevel;
        Defence = stat.defence;
        Speed = stat.speed;
        expReward = stat.expReward;
        isBoss = stat.isBoss;
    }
}
