using UnityEngine;

public class AmbushSkill : PlayerSkill
{
    private const float damage_Mult = 1.5f;
    private const float guard_Break_Mult = 2.5f;

    public AmbushSkill()
    {
        SkillName = "Ambush";
        MpCost = 60;
        Cooldown = 2;
        Price = 220;
        RequiresTarget = true;
        Icon = Resources.Load<Sprite>("Skills/Ambush");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        float mult = target.IsGuarding ? guard_Break_Mult : damage_Mult;
        int damage = caster.GetDamage(mult);

        if (target.IsGuarding) target.IsGuarding = false;

        target.TakeDamage(damage);
        StartCooldown();
    }
}
