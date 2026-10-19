using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyIntent { Attack, Guard, Skill, Stunned }

public class EnemyData : Character
{
    public string EnemyName;
    public int ExpReward;
    public bool IsBoss;
    public int StageIndex = 1;
    public List<EnemySkill> Skills = new();

    public EnemyIntent Intent { get; private set; }
    public EnemySkill PlannedSkill { get; private set; }
    public Action<EnemyData> OnIntentChanged;

    private bool phaseDone;

    public void SetStats(int level, EnemyStat stat)
    {
        this.Level = level;
        EnemyName = stat.enemyName;
        MaxHP = stat.baseHp + level * stat.hpPerLevel;
        HP = MaxHP;
        Damage = stat.baseDamage + level * stat.damagePerLevel;
        Defence = stat.defence;
        Speed = stat.speed;
        ExpReward = stat.expReward;
        IsBoss = stat.isBoss;
    }

    // 다음 행동을 미리 정함 (플레이어 턴 동안 UI에 표시). 방어는 즉시 발동.
    public void PlanAction()
    {
        PlannedSkill = null;
        IsGuarding = false;

        if (StunTurns > 0) Intent = EnemyIntent.Stunned;
        else
        {
            PlannedSkill = Skills.Find(s => s.CanUse());
            if (PlannedSkill != null) Intent = EnemyIntent.Skill;
            else if (UnityEngine.Random.value < 0.7f) Intent = EnemyIntent.Attack;
            else { Intent = EnemyIntent.Guard; SetGuard(Level); }
        }
        OnIntentChanged?.Invoke(this);
    }

    public string IntentText()
    {
        switch (Intent)
        {
            case EnemyIntent.Guard: return "방어";
            case EnemyIntent.Skill: return PlannedSkill != null ? PlannedSkill.SkillName + "!" : "스킬";
            case EnemyIntent.Stunned: return "행동불능";
            default: return "공격";
        }
    }

    // 보스 체력 50% 이하 시 1회 발동
    public void CheckPhase(PlayerData player)
    {
        if (!IsBoss || phaseDone || HP > MaxHP / 2) return;
        phaseDone = true;
        if (StageIndex == 2)
        {
            player.AttackMult = 0.7f;
            player.AttackMultTurns = 4 + 1;   // 턴 종료 시 바로 1 감소하므로 +1
        }
        else if (StageIndex == 3)
        {
            Heal(MaxHP / 4);
            DefenceDebuff.Add(30f);
            DefenceDebuffTurns.Add(9999);
            Damage += 20;
        }
    }
}
