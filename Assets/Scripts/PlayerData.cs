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
    private int weaponDamage;
    private int armorDefence;
    public PlayerSkill[] EquippedSkills = new PlayerSkill[4];
    public List<PlayerSkill> LearnedSkills = new();
    public Dictionary<Potion, int> Potions = new();
    public Weapon EquippedWeapon;
    public List<Weapon> OwnedWeapons = new();
    public Armor EquippedArmor;
    public List<Armor> OwnedArmors = new();
    public int Exp => exp;
    public int Gold => gold;
    public int MaxMP => maxMp;
    public int MP => mp;
    public int CritChance => critChance;
    public int DodgeChance => dodgeChance;

    private const int MAX_LEVEL = 10;

    public PlayerData()
    {
        ResetPlayerStats();
        InitPotions();
        EquipSkill(0, new SlashSkill());
        EquipSkill(1, new CleaveSkill());
    }

    private void InitPotions()
    {
        Potions[new RedPotion()] = 5;
        Potions[new BluePotion()] = 5;
        Potions[new StrengthPotion()] = 1;
        Potions[new ManaPotion()] = 1;
        Potions[new DodgePotion()] = 1;
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
        Heal(10);
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

    public bool TryUsePotion(Potion potion)
    {
        if (!Potions.ContainsKey(potion) || Potions[potion] <= 0) return false;
        Potions[potion]--;
        potion.Use(this);
        return true;
    }

    public void AddPotion(Potion potion, int amount)
    {
        if (!Potions.ContainsKey(potion)) return;
        Potions[potion] = Mathf.Min(potion.MaxStack, Potions[potion] + amount);
    }

    public void AddWeapon(Weapon weapon)
    {
        OwnedWeapons.Add(weapon);
    }

    public void AddArmor(Armor armor)
    {
        OwnedArmors.Add(armor);
    }

    public void EquipWeapon(Weapon weapon)
    {
        EquippedWeapon = weapon;
        weaponDamage = weapon.AttackBonus;
    }

    public void UnequipWeapon()
    {
        EquippedWeapon = null;
        weaponDamage = 0;
    }

    public void EquipArmor(Armor armor)
    {
        EquippedArmor = armor;
        armorDefence = armor.DefenceBonus;
    }

    public void UnequipArmor()
    {
        EquippedArmor = null;
        armorDefence = 0;
    }

    public int GetDamage(float mult)
    {
        return Mathf.RoundToInt((Damage + AttackBonus + weaponDamage) * mult * GetCri());
    }

    public int GetCri()
    {
        return Random.Range(0, 101) < CritChance + CritBonus ? 2 : 1;
    }

    public void UseMp(int mpcost)
    {
        mp = Mathf.Max(0, mp - Mathf.RoundToInt( mpcost - mpcost * MpCostReduction));
        OnStatChanged?.Invoke(this);
    }

    public void FillMp(int amount)
    {
        mp = Mathf.Min(MaxMP, mp + amount);
        OnStatChanged?.Invoke(this);
    }

    public void Heal(int amount)
    {
        HP = Mathf.Min(MaxHP, HP + amount);
        OnStatChanged?.Invoke(this);
    }

    public void OnTurnEnd()
    {
        foreach (var skill in EquippedSkills)
        {
            skill?.ReduceCooldown();
        }
        ReduceBuffTurns();
        mp = Mathf.Min(MaxMP, mp + 10);
        OnStatChanged?.Invoke(this);
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