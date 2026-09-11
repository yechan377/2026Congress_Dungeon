using UnityEngine;

public class StrengthPotion : Potion
{
    public StrengthPotion()
    {
        PotionName = "Strength Potion";
        Description = "+30% ATK\n(3 turns)";
        Price = 150;
        MaxStack = 1;
        Icon = Resources.Load<Sprite>("Potion/StrengthPotion");
    }

    public override void Use(PlayerData caster)
    {
        caster.AttackBonus = (caster.Damage + caster.WeaponDamage) *  0.3f;
        caster.AttackBonusTurns = 3;
    }
}