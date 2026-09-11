using UnityEngine;

public class FireballSkill : PlayerSkill
{
    private const float damage_Mult = 1.8f;

    public FireballSkill()
    {
        SkillName = "Fireball";
        MpCost = 65;
        Cooldown = 0;
        Price = 200;
        Icon = Resources.Load<Sprite>("Skills/FireBall");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        int damage = Mathf.RoundToInt(caster.GetDamage(damage_Mult));

        foreach (var enemy in BattleManager.Instance.EnemySlot)
        {
            if (enemy.IsDead()) continue;
            enemy.TakeDamage(damage);
        }
        StartCooldown();
    }
}