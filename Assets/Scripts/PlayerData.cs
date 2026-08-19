using System.Collections.Generic;
using UnityEngine;
public class PlayerData : Character
{
    private int exp;
    private int gold;
    private int maxMp;
    private int mp;
    private int critChance;
    private int dodgeChance;
    public PlayerSkill[] EquippedSkills = new PlayerSkill[4];
    public List<PlayerSkill> LearnedSkills = new();
    public int Exp => exp;
    public int Gold => gold;
    public int MaxMP => maxMp;
    public int MP => mp;
    public int CritChance => critChance;
    public int DodgeChance => dodgeChance;
    public int CritBonus;
    public int CritBonusTurn;

    private const int MAX_LEVEL = 10;

    public PlayerData()
    {
        ResetPlayerStats();
        EquipSkill(0, new SlashSkill());
        EquipSkill(1, new CleaveSkill());
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
        critChance = 10;
        dodgeChance = 15;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnStatChanged?.Invoke(this);
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
        OnStatChanged?.Invoke(this);
    }

    public int GetExpToNextLevel()
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
        OnStatChanged?.Invoke(this);

        LearnSkills(Level);
    }

    public void EquipSkill(int slotIndex, PlayerSkill skill)
    {
        if (slotIndex < 0 || slotIndex >= EquippedSkills.Length) return;
        EquippedSkills[slotIndex] = skill;
    }

    public void OnTurnEnd()
    {
        foreach (var skill in EquippedSkills)
        {
            skill?.ReduceCooldown();
        }
        ReduceBuffTurns();
    }

    public void ReduceBuffTurns()
    {
        if (CritBonusTurn == 0) return;
        CritBonusTurn -= 1;
        if (CritBonusTurn == 0) CritBonus = 0;
    }

    private void LearnSkills(int level)
    {
        switch (level) {
            case 2:
                LearnSkill(new GlareSkill());
                LearnSkill(new MeditateSkill());
                break;
            case 4:
                LearnSkill(new UltimateSkill());
                break;
            case 6:
                LearnSkill(new DoubleActionSkill());
                break;
        }
        GameObject.FindFirstObjectByType<BattleUI>().RefreshButtons();
    }

    private void LearnSkill(PlayerSkill skill)
    {
        LearnedSkills.Add(skill);

        int emptySlot = FindEmptySlot();
        if (emptySlot != -1)
        {
            EquipSkill(emptySlot, skill);
        }
        else
        {
            AskSkillReplace(skill);
        }
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < EquippedSkills.Length; i++)
        {
            if (EquippedSkills[i] == null) return i;
        }
        return -1;
    }

    private void AskSkillReplace(PlayerSkill newSkill)
    {
    }
}