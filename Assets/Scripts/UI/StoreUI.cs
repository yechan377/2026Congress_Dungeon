using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private StoreSlot[] gridSlots;
    [SerializeField] private TMP_Text goldText;

    private enum TabType { Item, Potion, Skill }
    private TabType currentTab = TabType.Item;
    private PlayerData player;

    private void Start()
    {
        player = BattleManager.Instance.CurrentPlayer;
        player.OnStatChanged += OnGoldChanged;
        goldText.text = $"Gold : {player.Gold}";
        OnClickItemTab();
        CreateRestButton();
    }

    private void CreateRestButton()
    {
        var canvas = goldText.GetComponentInParent<Canvas>();
        if (canvas == null) return;
        Button btn = null;
        btn = RuntimeUI.Button(canvas.transform, "휴식 (HP 30% 회복)", new Vector2(0.5f, 1f), new Vector2(0, -60), new Vector2(420, 70), new Color(0.2f, 0.55f, 0.3f), () =>
        {
            player.Heal(Mathf.RoundToInt(player.MaxHP * 0.3f));
            btn.interactable = false;
        }, 28);
    }

    private void OnGoldChanged(Character character)
    {
        goldText.text = $"Gold : {player.Gold}";
    }

    public void OnClickItemTab()
    {
        currentTab = TabType.Item;
        RefreshGrid();
    }

    public void OnClickPotionTab()
    {
        currentTab = TabType.Potion;
        RefreshGrid();
    }

    public void OnClickSkillTab()
    {
        currentTab = TabType.Skill;
        RefreshGrid();
    }

    public void OnSeclectScene()
    {
        GameManager.Instance.SceneSelectScene();
    }

    private void RefreshGrid()
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var (item, price, owned) = GetShopItemAt(i);

            if (item == null)
            {
                gridSlots[i].Clear();
            }
            else
            {
                gridSlots[i].SetItem(item, price, owned);
            }
        }
    }

    private (object item, int price, bool owned) GetShopItemAt(int index)
    {
        PlayerData player = GameManager.Instance.CurrentPlayer;

        switch (currentTab)
        {
            case TabType.Potion:
                {
                    var potions = player.Potions.Keys.ToList();
                    if (index < potions.Count)
                    {
                        Potion potion = potions[index];
                        return (potion, potion.Price, false);
                    }
                    return (null, 0, false);
                }

            case TabType.Skill:
                {
                    var shopSkills = StoreSkillDatabase.All;
                    if (index < shopSkills.Count)
                    {
                        PlayerSkill skill = shopSkills[index];
                        bool owned = player.LearnedSkills.Contains(skill);
                        return (skill, skill.Price, owned);
                    }
                    return (null, 0, false);
                }

            case TabType.Item:
                {
                    var allEquip = GetAllShopEquip();
                    if (index < allEquip.Count)
                    {
                        object equip = allEquip[index];
                        bool owned = IsEquipOwned(player, equip);
                        int price = GetEquipPrice(equip);
                        return (equip, price, owned);
                    }
                    return (null, 0, false);
                }
        }
        return (null, 0, false);
    }

    private List<object> GetAllShopEquip()
    {
        List<object> list = new();
        list.AddRange(WeaponDatabase.All);
        list.AddRange(ArmorDatabase.All);
        return list;
    }

    private bool IsEquipOwned(PlayerData player, object equip)
    {
        if (equip is Weapon w) return player.OwnedWeapons.Contains(w);
        if (equip is Armor a) return player.OwnedArmors.Contains(a);
        return false;
    }

    private int GetEquipPrice(object equip)
    {
        if (equip is Weapon w) return w.Price;
        if (equip is Armor a) return a.Price;
        return 0;
    }
}