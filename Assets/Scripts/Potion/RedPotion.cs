using UnityEngine;

public class RedPotion : Potion
{
    private const float HEAL_RATE = 0.2f;

    public RedPotion()
    {
        PotionName = "Red Potion";
        Description = "Restores 20% HP";
        Price = 30;
        MaxStack = 5;
        Icon = Resources.Load<Sprite>("Potion/RedPotion");
    }

    public override void Use(PlayerData caster)
    {
        int healAmount = Mathf.RoundToInt(caster.MaxHP * HEAL_RATE);
        caster.Heal(healAmount);
    }
}
