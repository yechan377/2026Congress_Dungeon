using UnityEngine;

public class CleaveSkill : PlayerSkill
{
    private const float damage_Mult = 1.4f;

    public CleaveSkill()
    {
        SkillName = "Cleave";
        MpCost = 35;
        Cooldown = 0;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        int damage = caster.GetDamage(damage_Mult);

        foreach (var enemy in BattleManager.Instance.EnemySlot)
        {
            if (enemy.IsDead()) continue;
            enemy.TakeDamage(damage);
        }
    }
}