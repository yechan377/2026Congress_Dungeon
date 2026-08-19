using UnityEngine;

public class MeditateSkill : PlayerSkill
{
    public MeditateSkill()
    {
        SkillName = "Meditate";
        MpCost = 45;
        Cooldown = 5;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        caster.HP += Mathf.RoundToInt(caster.MaxMP * 0.3f);
        StartCooldown();
    }
}
