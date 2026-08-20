using System;
using UnityEngine;
public class Character
{
    public int Level, MaxHP, HP, Damage, Defence, Speed;

    public int CritBonus;
    public int CritBonusTurns;

    public float AttackBonus;
    public int AttackBonusTurns;

    public float MpCostReduction;
    public int MpCostReductionTurns;

    public float EvasionMultiplier = 1f;
    public int EvasionMultiplierTurns;

    public Action<Character> OnStatChanged;

    public void TakeDamage(int amount)
    {
        bool wasAlive = !IsDead();
        HP = Mathf.Max(0, HP - amount);
        OnStatChanged?.Invoke(this);
    }
    public bool IsDead() => HP <= 0;

    public void ReduceBuffTurns()
    {
        if (CritBonusTurns > 0) { CritBonusTurns--; if (CritBonusTurns <= 0) CritBonus = 0; }
        if (AttackBonusTurns > 0) { AttackBonusTurns--; if (AttackBonusTurns <= 0) AttackBonus = 0f; }
        if (MpCostReductionTurns > 0) { MpCostReductionTurns--; if (MpCostReductionTurns <= 0) MpCostReduction = 0f; }
        if (EvasionMultiplierTurns > 0) { EvasionMultiplierTurns--; if (EvasionMultiplierTurns <= 0) EvasionMultiplier = 1f; }
    }
}