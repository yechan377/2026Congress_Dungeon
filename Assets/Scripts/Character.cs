using UnityEngine;
public class Character
{
    public int Level, MaxHP, HP, Damage, Defence, Speed;
    public int GetAttackDamage() => Damage;
    public void TakeDamage(int amount)
    {
        bool wasAlive = !IsDead();
        HP = Mathf.Max(0, HP - amount);
        if (wasAlive && IsDead())
        {
            BattleManager.Instance.CheckDeath(this);
        }
    }
    public bool IsDead() => HP <= 0;
}