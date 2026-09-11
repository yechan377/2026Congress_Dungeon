using UnityEngine;

public class LastStandSkill : PlayerSkill
{
    public LastStandSkill()
    {
        SkillName = "Last Stand";
        MpCost = 45;
        Cooldown = 0;
        Price = 150;
        Icon = Resources.Load<Sprite>("Skills/LastStand");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        float lostRatio = 1f - ((float)caster.HP / caster.MaxHP);
        float multiplier = Mathf.Lerp(1.5f, 2.5f, lostRatio);
        int damage = caster.GetDamage(multiplier);
        target.TakeDamage(damage);
        StartCooldown();
    }
}