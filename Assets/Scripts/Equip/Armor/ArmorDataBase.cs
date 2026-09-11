using System.Collections.Generic;
using UnityEngine;

public static class ArmorDatabase
{
    public static readonly Armor ClothArmor = new("Cloth Armor", 3, 40, Resources.Load<Sprite>("Equipment/ClothArmor"));
    public static readonly Armor LeatherArmor = new("Leather Armor", 6, 90, Resources.Load<Sprite>("Equipment/LeatherArmor"));
    public static readonly Armor ChainArmor = new("Chain Armor", 10, 180, Resources.Load<Sprite>("Equipment/ChainArmor"));
    public static readonly Armor IronArmor = new("Iron Armor", 15, 320, Resources.Load<Sprite>("Equipment/IronArmor"));
    public static readonly Armor PlateArmor = new("Plate Armor", 22, 600, Resources.Load<Sprite>("Equipment/PlateArmor"));

    public static readonly List<Armor> All = new() {
        ClothArmor, LeatherArmor, ChainArmor, IronArmor, PlateArmor
    };
}