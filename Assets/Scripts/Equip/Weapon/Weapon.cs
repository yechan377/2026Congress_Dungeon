using UnityEngine;

public class Weapon : EquipMent
{
    public int AttackBonus { get; private set; }

    public Weapon(string name, int attackbonus, int price, Sprite icon)
    {
        ItemName = name;
        AttackBonus = attackbonus;
        Price = price;
        Icon = icon;
    }
}
