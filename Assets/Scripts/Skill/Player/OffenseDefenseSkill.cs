using UnityEngine;

public class OffenseDefenseSkill : PlayerSkill
{


    public OffenseDefenseSkill()
    {
        SkillName = "OffenseDefense";
        MpCost = 100;
        Cooldown = 3;
        Price = 300;
        Icon = Resources.Load<Sprite>("Skills/OffenseDefense");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        
    }
}
