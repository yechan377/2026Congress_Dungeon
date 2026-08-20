using UnityEngine;

public class Weapon : EquipMent
{
    public int AttackBonus { get; set; }

    public Weapon(string name, int attackbonus, int price)
    {
        ItemName = name;
        AttackBonus = attackbonus;
        Price = price;
    }
}
