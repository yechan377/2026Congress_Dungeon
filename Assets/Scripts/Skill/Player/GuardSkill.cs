using UnityEngine;

public class GuardSkill : PlayerSkill
{
    private const int defence_Bonus = 15;
    private const int defence_Bonus_Turn = 2;

    public GuardSkill()
    {
        SkillName = "Guard";
        MpCost = 30;
        Cooldown = 0;
        Price = 100;
        RequiresTarget = false;
        Icon = Resources.Load<Sprite>("Skills/Guard");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        caster.DefenceBonus.Add(defence_Bonus);
        caster.DefenceBonusTurns.Add(defence_Bonus_Turn);
        StartCooldown();
    }
}