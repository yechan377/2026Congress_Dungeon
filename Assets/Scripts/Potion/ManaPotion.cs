using UnityEngine;

public class ManaPotion : Potion
{
    public ManaPotion()
    {
        PotionName = "Mana Potion";
        Description = "-50% MP cost\n(3 turns)";
        Price = 150;
        MaxStack = 1;
        Icon = Resources.Load<Sprite>("Potion/ManaPotion");
    }

    public override void Use(PlayerData caster)
    {
        caster.MpCostReduction = 0.5f;
        caster.MpCostReductionTurns = 3;
    }
}
