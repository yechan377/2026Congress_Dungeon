using UnityEngine;

public class DodgePotion : Potion
{
    public DodgePotion()
    {
        PotionName = "Dodge Potion";
        Description = "x2 Dodge\n(3 turns)";
        Price = 150;
        MaxStack = 1;
        Icon = Resources.Load<Sprite>("Potion/DodgePotion");
    }

    public override void Use(PlayerData caster)
    {
        caster.EvasionMultiplier = 2;
        caster.EvasionMultiplierTurns = 3;
    }
}
