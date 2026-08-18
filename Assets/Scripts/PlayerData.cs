using UnityEngine;

public class PlayerData : Character
{
    private int exp;
    private int gold;
    private int maxMp;
    private int mp;
    private float critChance;
    private float dodgeChance;

    public int Exp => exp;
    public int Gold => Gold;
    public int MaxMP => maxMp;
    public int MP => mp;
    public float CritChance => critChance;
    public float DodgeChance => dodgeChance;

    private const int MAX_LEVEL = 10;

    public PlayerData()
    {
        ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        Level = 1;
        exp = 0;
        gold = 0;
        MaxHP = 100;
        HP = 100;
        maxMp = 50;
        mp = 50;
        Damage = 20;
        Defence = 10;
        Speed = 10;
        critChance = 0.1f;
        dodgeChance = 0.15f;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void AddExp(int amount)
    {
        if (Level >= MAX_LEVEL) return;

        exp += amount;

        while (exp >= GetExpToNextLevel() && Level < MAX_LEVEL)
        {
            exp -= GetExpToNextLevel();
            LevelUp();
        }
    }

    private int GetExpToNextLevel()
    {
        return Level * 100;
    }

    private void LevelUp()
    {
        Level += 1;
        MaxHP += 20;
        HP = Mathf.Min(MaxHP, HP + 10);
        maxMp += 10;
        mp = maxMp;
        Damage += 10;
    }
}
