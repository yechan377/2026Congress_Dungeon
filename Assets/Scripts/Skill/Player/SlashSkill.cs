using UnityEngine;

public class SlashSkill : PlayerSkill
{
    private const float damage_Mult = 1.7f;

    public SlashSkill()
    {
        SkillName = "Slash";
        MpCost = 30;
        Cooldown = 0;
        Icon = Resources.Load<Sprite>("Skills/Slash");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        int damage = caster.GetDamage(damage_Mult);
        target.TakeDamage(damage);
        StartCooldown();
    }
}
