using UnityEngine;

public class UltimateSkill : PlayerSkill
{
    private const float damage_Mult = 3.0f;

    public UltimateSkill()
    {
        SkillName = "Ultimate";
        MpCost = 100;
        Cooldown = 10;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        int damage = Mathf.RoundToInt(caster.Damage * damage_Mult * GetCri(caster));

        foreach (var enemy in BattleManager.Instance.EnemySlot)
        {
            if (enemy.IsDead()) continue;
            enemy.TakeDamage(damage);
        }
    }
}
