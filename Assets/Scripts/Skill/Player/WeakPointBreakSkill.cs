using UnityEngine;

public class WeakPointBreakSkill : PlayerSkill
{
    private const float damage_Mult = 1.3f;
    private const float defence_Debuff = 0.1f;
    private const int defence_DebuffTurn = 3;

    public WeakPointBreakSkill()
    {
        SkillName = "Weak Point Break";
        MpCost = 55;
        Cooldown = 2;
        Price = 180;
        Icon = Resources.Load<Sprite>("Skills/WeakPointBreak");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        int damage = caster.GetDamage(damage_Mult);
        target.TakeDamage(damage);

        target.DefenceDebuff.Add(defence_Debuff);
        target.DefenceDebuffTurns.Add(defence_DebuffTurn);

        StartCooldown();
    }
}
