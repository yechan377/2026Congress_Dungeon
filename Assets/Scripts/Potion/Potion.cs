using UnityEngine;

public abstract class Potion
{
    public string PotionName { get; protected set; }
    public string Description { get; protected set; }
    public int Price { get; protected set; }
    public int MaxStack { get; protected set; }
    public Sprite Icon { get; protected set; }

    public abstract void Use(PlayerData caster);
}