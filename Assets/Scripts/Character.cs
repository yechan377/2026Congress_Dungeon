using System;
using System.Collections.Generic;
using UnityEngine;
public class Character
{
    private const float DEFENCE_CONST = 100f;

    public int Level, MaxHP, HP, Damage, Defence, Speed;

    public int CritBonus;
    public int CritBonusTurns;

    public float AttackBonus;
    public int AttackBonusTurns;

    public float MpCostReduction = 1f;
    public int MpCostReductionTurns;

    public float EvasionMultiplier = 1f;
    public int EvasionMultiplierTurns;

    public List<float> DefenceBonus = new();
    public List<int> DefenceBonusTurns = new();

    public List<float> DefenceDebuff = new();
    public List<int> DefenceDebuffTurns = new();

    public bool IsGuarding;
    public float GuardReductionRate;

    public Action<Character> OnStatChanged;

    public void TakeDamage(int amount)
    {
        bool wasAlive = !IsDead();

        int armorDefence = (this is PlayerData p) ? p.ArmorDefence : 0;
        float defencebonus = 0;
        float defencedebuff = 0;
        if (DefenceBonus.Count > 1 && DefenceBonus.Count != 0)
        {
            for (int i = 0; i < DefenceBonus.Count; i++)
            {
                defencebonus += DefenceBonus[i];
            }
        }
        else defencebonus = 0;
        if (DefenceDebuff.Count > 1 && DefenceDebuff.Count != 0)
        {
            for (int i = 0; i < DefenceDebuff.Count; i++)
            {
                defencedebuff += DefenceDebuff[i];
            }
        }
        else defencedebuff = 0;
        float totalDefenceStat = Defence + armorDefence + defencebonus - defencedebuff;
        float defenceRate = totalDefenceStat / (totalDefenceStat + DEFENCE_CONST);

        int damage = Mathf.RoundToInt(amount * (1f - defenceRate));
        damage = Mathf.Max(1, damage);

        if (IsGuarding)
        {
            damage = Mathf.RoundToInt(damage * (1f - GuardReductionRate));
            IsGuarding = false;
        }

        HP = Mathf.Max(0, HP - damage);
        OnStatChanged?.Invoke(this);
    }

    public virtual int GetDamage(float mult)
    {
        return Mathf.RoundToInt((Damage + AttackBonus) * mult);
    }

    public void SetGuard(int level)
    {
        IsGuarding = true;
        GuardReductionRate = (50f + level * 3f) / 100f;
    }

    public bool IsDead() => HP <= 0;

    public void ReduceBuffTurns()
    {
        if (CritBonusTurns > 0) { CritBonusTurns--; if (CritBonusTurns <= 0) CritBonus = 0; }
        if (AttackBonusTurns > 0) { AttackBonusTurns--; if (AttackBonusTurns <= 0) AttackBonus = 0f; }
        if (DefenceBonusTurns.Count > 0)
        {
            for(int i = DefenceBonusTurns.Count - 1; i >= 0; i--)
            {
                DefenceBonusTurns[i]--;
                if (DefenceBonusTurns[i] == 0)
                {
                    DefenceBonusTurns.RemoveAt(i);
                    DefenceBonus.RemoveAt(i);
                }
            }
        }
        if (MpCostReductionTurns > 0) { MpCostReductionTurns--; if (MpCostReductionTurns <= 0) MpCostReduction = 1f; }
        if (EvasionMultiplierTurns > 0) { EvasionMultiplierTurns--; if (EvasionMultiplierTurns <= 0) EvasionMultiplier = 1f; }
    }

    public void ReduceDeBuffTurns()
    {
        if (DefenceDebuffTurns.Count > 0)
        {
            for (int i = DefenceDebuffTurns.Count - 1; i >= 0; i--)
            {
                DefenceDebuffTurns[i]--;
                if (DefenceDebuffTurns[i] == 0)
                {
                    DefenceDebuffTurns.RemoveAt(i);
                    DefenceDebuff.RemoveAt(i);
                }
            }
        }
    }

    public void ResetBattleState()
    {
        CritBonus = 0; CritBonusTurns = 0;
        AttackBonus = 0; AttackBonusTurns = 0;
        MpCostReduction = 0f; MpCostReductionTurns = 0;
        EvasionMultiplier = 1f; EvasionMultiplierTurns = 0;
        DefenceBonus = new(); DefenceBonusTurns = new();
        DefenceDebuff = new(); DefenceDebuffTurns = new();
        IsGuarding = false;
    }
}