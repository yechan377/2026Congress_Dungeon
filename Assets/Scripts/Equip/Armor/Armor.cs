public class Armor : EquipMent
{
    public int DefenceBonus { get; private set; }

    public Armor(string name, int defencebonus, int price)
    {
        ItemName = name;
        DefenceBonus = defencebonus;
        Price = price;
    }
}
