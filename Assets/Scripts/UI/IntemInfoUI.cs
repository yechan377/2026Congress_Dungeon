using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Multiplayer.PlayMode;
using UnityEngine;
using static UnityEditor.Progress;

public class IntemInfoUI : MonoBehaviour
{
    public static IntemInfoUI Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    private object currentItem;
    private PlayerData player;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        player = GameManager.Instance.CurrentPlayer;
    }

    public void Show(object item, Vector3 slotPosition)
    {
        currentItem = item;
        panel.SetActive(true);
        panel.transform.position = new Vector3(slotPosition.x + 160f, slotPosition.y - 206f, slotPosition.z);

        switch (item)
        {
            case Weapon weapon:
                nameText.text = weapon.ItemName;
                descriptionText.text = $"+{weapon.AttackBonus} ATK";
                break;
            case Armor armor:
                nameText.text = armor.ItemName;
                descriptionText.text = $"+{armor.DefenceBonus} DEF";
                break;
            case PlayerSkill skill:
                nameText.text = skill.SkillName;
                descriptionText.text = $"MP {skill.MpCost}";
                break;
            case Potion potion:
                nameText.text = potion.PotionName;
                descriptionText.text = potion.Description;
                break;
        }
    }

    public void OnEquipItem()
    {
        switch (currentItem)
        {
            case Weapon weapon:
                player.EquipWeapon(weapon);
                RightSideUI.Instance.EquipWeapon();
                break;
            case Armor armor:
                player.EquipArmor(armor);
                RightSideUI.Instance.EquipArmor();
                break;
            case PlayerSkill skill:
                SkillConfirmUI.Instance.ShowReplaceConfirm(skill);
                break;
        }
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}