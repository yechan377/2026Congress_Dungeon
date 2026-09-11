using UnityEngine;

public class BluePotion : Potion
{
    private const float MP_RATE = 0.2f;

    public BluePotion()
    {
        PotionName = "Blue Potion";
        Description = "Restores 20% MP";
        Price = 30;
        MaxStack = 5;
        Icon = Resources.Load<Sprite>("Potion/BluePotion");
    }

    public override void Use(PlayerData caster)
    {
        int mpAmount = Mathf.RoundToInt(caster.MaxMP * MP_RATE);
        caster.FillMp(mpAmount);
    }
}