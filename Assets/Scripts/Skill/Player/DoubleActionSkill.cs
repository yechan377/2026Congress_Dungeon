using UnityEngine;

public class DoubleActionSkill : PlayerSkill
{
    public DoubleActionSkill()
    {
        SkillName = "Two Heart";
        MpCost = 0;
        Cooldown = 10;
    }

    public override void Execute(PlayerData caster, Character target)
    {
        BattleManager.Instance.OnTwoAction();
    }
}
