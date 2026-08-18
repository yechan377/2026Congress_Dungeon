using UnityEngine;

public class EnemyData : Character
{
    public int ExpReward;
    public bool IsBoss;

    public void SetStats(int level, EnemyStat stat)
    {
        this.Level = level;
        MaxHP = stat.baseHp + level * stat.hpPerLevel;
        HP = MaxHP;
        Damage = stat.baseDamage + level * stat.damagePerLevel;
        Defence = stat.defence;
        Speed = stat.speed;
        ExpReward = stat.expReward;
        IsBoss = stat.isBoss;
    }
}
