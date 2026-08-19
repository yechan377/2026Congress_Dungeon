using UnityEngine;

public class GlareSkill : PlayerSkill
{
    public GlareSkill()
    {
        SkillName = "Glare";
        MpCost = 25;
        Cooldown = 0;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        caster.CritBonus = 25;
        caster.CritBonusTurn = 3;
    }
}
