using TMPro;
using UnityEngine;

public class IntemInfoUI : MonoBehaviour
{
    public static IntemInfoUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(object item, Vector3 slotPosition)
    {
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

    public void Hide()
    {
        panel.SetActive(false);
    }
}