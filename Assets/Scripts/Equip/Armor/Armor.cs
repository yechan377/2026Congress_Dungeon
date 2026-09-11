using UnityEngine;

public class Armor : EquipMent
{
    public int DefenceBonus { get; private set; }

    public Armor(string name, int defencebonus, int price, Sprite icon)
    {
        ItemName = name;
        DefenceBonus = defencebonus;
        Price = price;
        Icon = icon;
    }
}
