using System;
using UnityEngine;
public class Character
{
    public int Level, MaxHP, HP, Damage, Defence, Speed;

    public Action<Character> OnStatChanged;

    public void TakeDamage(int amount)
    {
        bool wasAlive = !IsDead();
        HP = Mathf.Max(0, HP - amount);
        OnStatChanged?.Invoke(this);
    }
    public bool IsDead() => HP <= 0;
}