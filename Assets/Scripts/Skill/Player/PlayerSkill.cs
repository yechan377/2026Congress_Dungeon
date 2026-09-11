using UnityEngine;

public abstract class PlayerSkill : Skill
{
    public bool RequiresTarget { get; protected set; } = true;
    public int MpCost { get; protected set; }

    public int Price { get; protected set; }

    public bool CanUse(PlayerData caster)
    {
        return caster.MP >= MpCost * caster.MpCostReduction && IsReady();
    }

    public abstract void Execute(PlayerData caster, Character target);
}