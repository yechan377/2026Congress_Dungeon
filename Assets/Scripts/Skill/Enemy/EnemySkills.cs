using UnityEngine;

// 1스테이지 보스
public class SmashSkill : EnemySkill
{
    public SmashSkill() { SkillName = "내려찍기"; Cooldown = 8; }
    public override void Execute(Character c, Character t)
    {
        t.TakeDamage(c.GetDamage(1.8f));
        StartCooldown();
    }
}

// 2스테이지 보스
public class ConfuseStrikeSkill : EnemySkill
{
    public ConfuseStrikeSkill() { SkillName = "혼란의 일격"; Cooldown = 5; }
    public override void Execute(Character c, Character t)
    {
        t.TakeDamage(c.GetDamage(1.2f));
        if (t is PlayerData p) p.SealSkills(1);
        StartCooldown();
    }
}

public class AllOutStrikeSkill : EnemySkill
{
    public AllOutStrikeSkill() { SkillName = "혼신의 일격"; Cooldown = 9; }
    public override void Execute(Character c, Character t)
    {
        t.TakeDamage(c.GetDamage(2f));
        StartCooldown();
    }
}

// 3스테이지 보스
public class DarknessSkill : EnemySkill
{
    public DarknessSkill() { SkillName = "암흑"; Cooldown = 7; }
    public override void Execute(Character c, Character t)
    {
        t.TakeDamage(c.GetDamage(1.2f));
        if (t is PlayerData p) p.Stun(1);
        StartCooldown();
    }
}

public class SoulDrainSkill : EnemySkill
{
    public SoulDrainSkill() { SkillName = "영혼 흡수"; Cooldown = 4; }
    public override void Execute(Character c, Character t)
    {
        int before = t.HP;
        t.TakeDamage(c.GetDamage(1.7f));
        int dealt = before - t.HP;
        c.Heal(dealt / 2);
        StartCooldown();
    }
}

// 사용 후 보스 본인이 3턴 행동불능
public class DestroyBeamSkill : EnemySkill
{
    public DestroyBeamSkill() { SkillName = "파괴 광선"; Cooldown = 10; }
    public override void Execute(Character c, Character t)
    {
        t.TakeDamage(c.GetDamage(3f));
        c.StunTurns = 3;
        StartCooldown();
    }
}

// 3스테이지 정예 (기획서에 스킬 내용 없음 -> 임시)
public class EliteStrikeSkill : EnemySkill
{
    public EliteStrikeSkill() { SkillName = "강타"; Cooldown = 4; }
    public override void Execute(Character c, Character t)
    {
        t.TakeDamage(c.GetDamage(1.5f));
        StartCooldown();
    }
}
