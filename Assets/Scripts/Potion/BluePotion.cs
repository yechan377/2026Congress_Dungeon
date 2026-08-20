using UnityEngine;

public class BluePotion : Potion
{
    private const float MP_RATE = 0.2f;

    public BluePotion()
    {
        PotionName = "Blue Potion";
        Description = "Restores 20% MP";
        MaxStack = 5;
    }

    public override void Use(PlayerData caster)
    {
        int mpAmount = Mathf.RoundToInt(caster.MaxMP * MP_RATE);
        caster.FillMp(mpAmount);
    }
}