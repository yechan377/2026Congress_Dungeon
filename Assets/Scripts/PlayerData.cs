using UnityEngine;
using UnityEngine.Rendering;

public class PlayerData : Character
{
    private int exp;
    private int maxMp;
    private int mp;
    private float critChance;
    private float dodgeChance;

    public int MaxMP => maxMp;
    public int MP => mp;

    public PlayerData()
    {
        ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        Level = 1;
        exp = 0;
        MaxHP = 100;
        HP = MaxHP;
        maxMp = 50;
        mp = maxMp;
        Damage = 20;
        Defence = 10;
        Speed = 10;
        critChance = 0.1f;
        dodgeChance = 0.15f;
    }
}
