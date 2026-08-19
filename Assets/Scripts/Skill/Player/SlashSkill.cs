using UnityEngine;

public class SlashSkill : PlayerSkill
{
    private const float damage_Mult = 1.7f;

    public SlashSkill()
    {
        SkillName = "Slash";
        MpCost = 30;
        Cooldown = 0;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        int damage = Mathf.RoundToInt(caster.Damage * damage_Mult * GetCri(caster));
        target.TakeDamage(damage);
        StartCooldown();
    }
}
