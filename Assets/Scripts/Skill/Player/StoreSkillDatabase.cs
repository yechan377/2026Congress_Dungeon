using System.Collections.Generic;

public static class StoreSkillDatabase
{
    public static readonly GuardSkill Guard = new();
    public static readonly LastStandSkill LastStand = new();
    public static readonly WeakPointBreakSkill WeakPointBreak = new();
    public static readonly FireballSkill Fireball = new();
    public static readonly AmbushSkill Ambush = new();
    public static readonly FinalStrikeSkill FinalStrike = new();
    public static readonly OffenseDefenseSkill OffenseDefense = new();

    public static readonly List<PlayerSkill> All = new() {
        Guard, LastStand, WeakPointBreak, Fireball, Ambush, FinalStrike, OffenseDefense
    };
}