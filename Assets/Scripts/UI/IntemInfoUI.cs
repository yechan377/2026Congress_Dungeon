using TMPro;
using UnityEngine;

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

    public void Prepare()
    {
        for (Transform t = transform; t != null; t = t.parent) t.gameObject.SetActive(true);
        panel.SetActive(false);
    }

    // 정보창은 항상 화면 정중앙에 표시
    public void Show(object item)
    {
        currentItem = item;
        panel.SetActive(true);
        panel.transform.SetAsLastSibling();
        var rt = panel.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);
        var canvas = panel.GetComponentInParent<Canvas>();
        if (canvas != null) panel.transform.position = canvas.rootCanvas.transform.position;

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
                if (RightSideUI.Instance) RightSideUI.Instance.EquipWeapon();
                break;
            case Armor armor:
                player.EquipArmor(armor);
                if (RightSideUI.Instance) RightSideUI.Instance.EquipArmor();
                break;
            case PlayerSkill skill:
                SkillConfirmUI.Get().ShowReplaceConfirm(skill);
                break;
        }
        Hide();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}