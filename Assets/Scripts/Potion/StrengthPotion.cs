public class StrengthPotion : Potion
{
    public StrengthPotion()
    {
        PotionName = "Strength Potion";
        Description = "+30% ATK\n(3 turns)";
        MaxStack = 1;
    }

    public override void Use(PlayerData caster)
    {
        caster.AttackBonus = caster.Damage *  0.3f;
        caster.AttackBonusTurns = 3;
    }
}