using UnityEngine;

public class MeditateSkill : PlayerSkill
{
    public MeditateSkill()
    {
        SkillName = "Meditate";
        MpCost = 45;
        Cooldown = 5;
        RequiresTarget = false;
        Icon = Resources.Load<Sprite>("Skills/Meditate");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        caster.Heal(Mathf.RoundToInt(caster.MaxHP * 0.3f));
        StartCooldown();
    }
}