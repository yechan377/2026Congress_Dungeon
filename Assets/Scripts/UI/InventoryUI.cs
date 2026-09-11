using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot[] gridSlots;

    [SerializeField] private Button weaponSlotButton;
    [SerializeField] private Button armorSlotButton;

    private enum TabType { Equip, Skill, Potion }
    private TabType currentTab = TabType.Equip;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
    }

    private void ToggleInventory()
    {
        bool isOpen = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isOpen);
        if (isOpen) OnClickEquipTab();
    }

    public void OnClickWeaponSlot()
    {
        if (!GameManager.Instance.IsSceneSelectScene) return;
        GameManager.Instance.CurrentPlayer.UnequipWeapon();
        RightSideUI.Instance.UnequipWeapon();
    }

    public void OnClickArmorSlot()
    {
        if (!GameManager.Instance.IsSceneSelectScene) return;
        GameManager.Instance.CurrentPlayer.UnequipArmor();
        RightSideUI.Instance.UnequipArmor();
    }

    public void OnClickEquipTab()
    {
        currentTab = TabType.Equip;
        RefreshGrid();
    }

    public void OnClickSkillTab()
    {
        currentTab = TabType.Skill;
        RefreshGrid();
    }

    public void OnClickPotionTab()
    {
        currentTab = TabType.Potion;
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        PlayerData player = GameManager.Instance.CurrentPlayer;

        for (int i = 0; i < gridSlots.Length; i++)
        {
            object itemAtIndex = GetItemAt(player, i);

            if (itemAtIndex == null)
            {
                gridSlots[i].Clear();
            }
            else
            {
                Debug.Log($"인벤토리에 {itemAtIndex}등록");
                gridSlots[i].SetItem(itemAtIndex);
            }
        }
    }

    private object GetItemAt(PlayerData player, int index)
    {
        switch (currentTab)
        {
            case TabType.Equip:
                if (index < player.OwnedWeapons.Count) return player.OwnedWeapons[index];
                index -= player.OwnedWeapons.Count;
                if (index < player.OwnedArmors.Count) return player.OwnedArmors[index];
                return null;

            case TabType.Skill:
                return index < player.LearnedSkills.Count ? player.LearnedSkills[index] : null;

            case TabType.Potion:
                int i = 0;
                foreach (var pair in player.Potions)
                {
                    if (pair.Value <= 0) continue;
                    if (i == index) return pair.Key;
                    i++;
                }
                return null;
        }
        return null;
    }

    public void ShowItemInfo(InventorySlot slot, object item)
    {
        IntemInfoUI.Instance.Show(item, slot.transform.position);
    }
}