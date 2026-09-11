using UnityEngine;

public class DoubleActionSkill : PlayerSkill
{
    public DoubleActionSkill()
    {
        SkillName = "Two Heart";
        MpCost = 0;
        Cooldown = 10;
        RequiresTarget = false;
        Icon = Resources.Load<Sprite>("Skills/DoubleAction");
    }

    public override void Execute(PlayerData caster, Character target)
    {
        BattleManager.Instance.OnTwoAction();
    }
}
