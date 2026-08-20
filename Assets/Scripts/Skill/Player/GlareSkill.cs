using UnityEngine;

public class GlareSkill : PlayerSkill
{
    public GlareSkill()
    {
        SkillName = "Glare";
        MpCost = 25;
        Cooldown = 0;
        RequiresTarget = false;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        caster.CritBonus = 25;
        caster.CritBonusTurns = 3;
    }
}
