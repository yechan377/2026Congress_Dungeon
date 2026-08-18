using UnityEngine;
public class Character
{
    public int Level, MaxHP, HP, Damage, Defence, Speed;
    public int GetAttackDamage() => Damage;
    public void TakeDamage(int amount)
    {
        bool wasAlive = !IsDead();
        HP = Mathf.Max(0, HP - amount);
        EventManager.Instance.Notification(EventType.OnHpChanged, null, this);
    }
    public bool IsDead() => HP <= 0;
}