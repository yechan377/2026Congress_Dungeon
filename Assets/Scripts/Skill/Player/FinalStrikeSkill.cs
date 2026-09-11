using UnityEngine;

public class FinalStrikeSkill : PlayerSkill
{
    private const float damage_Mult = 1.7f;
    private const float low_Hp_Mult = 3.0f;
    private const float low_Hp_enemy = 0.3f;

    public FinalStrikeSkill()
    {
        SkillName = "Final Strike";
        MpCost = 70;
        Cooldown = 1;
        Price = 250;
        Icon = Resources.Load<Sprite>("Skills/FinalStrike");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        float hpRatio = (float)target.HP / target.MaxHP;
        float mult = hpRatio <= low_Hp_enemy ? low_Hp_Mult : damage_Mult;
        int damage = caster.GetDamage(mult);
        target.TakeDamage(damage);
        StartCooldown();
    }
}