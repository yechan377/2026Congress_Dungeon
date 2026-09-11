using System.Collections.Generic;
using UnityEngine;

public static class WeaponDatabase
{
    public static readonly Weapon Dagger = new("Dagger", 5, 50, Resources.Load<Sprite>("Equipment/Dagger"));
    public static readonly Weapon Longsword = new("Longsword", 10, 100, Resources.Load<Sprite>("Equipment/LongSword"));
    public static readonly Weapon Axe = new("Axe", 15, 200, Resources.Load<Sprite>("Equipment/Axe"));
    public static readonly Weapon Greatsword = new("Greatsword", 20, 350, Resources.Load<Sprite>("Equipment/GreatSword"));
    public static readonly Weapon Hammer = new("Hammer", 25, 500, Resources.Load<Sprite>("Equipment/Hammer"));
    public static readonly Weapon MasterSword = new("Master Sword", 35, 800, Resources.Load<Sprite>("Equipment/MasterSword"));

    public static readonly List<Weapon> All = new() {
        Dagger, Longsword, Axe, Greatsword, Hammer, MasterSword
    };
}
