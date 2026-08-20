using UnityEngine;

public class RedPotion : Potion
{
    private const float HEAL_RATE = 0.2f;

    public RedPotion()
    {
        PotionName = "Red Potion";
        Description = "Restores 20% HP";
        MaxStack = 5;
    }

    public override void Use(PlayerData caster)
    {
        int healAmount = Mathf.RoundToInt(caster.MaxHP * HEAL_RATE);
        caster.Heal(healAmount);
    }
}
