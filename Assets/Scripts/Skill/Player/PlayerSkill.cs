using UnityEngine;

public abstract class PlayerSkill : Skill
{
    public int MpCost { get; protected set; }

    public bool CanUse(PlayerData caster)
    {
        return caster.MP >= MpCost && IsReady();
    }

    public int GetCri(PlayerData caster)
    {
        return Random.Range(0, 101) < caster.CritChance * 10 + caster.CritBonus? 2 : 1;
    }

    public abstract void Execute(PlayerData caster, Character target);
}