using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private Image[] skillIcons;
    [SerializeField] private TMP_Text[] skillname;
    [SerializeField] private TMP_Text[] mpcost;

    private void Awake()
    {
        RefreshButtons();
    }

    public void RefreshButtons()
    {
        PlayerData player = BattleManager.Instance.CurrentPlayer;
        if (player == null) return;

        for (int i = 0; i < skillButtons.Length; i++)
        {
            PlayerSkill skill = player.EquippedSkills[i];

            Image img = skillIcons[i];    
            Color nameColor = skillname[i].color;
            Color mpcostCorlr = mpcost[i].color;

            if (skill == null)
            {
                img.sprite = null;
                nameColor.a = 0f;
                mpcostCorlr.a = 0f;
                skillButtons[i].interactable = false;
            }
            else
            {
                img.sprite = skill.Icon;
                nameColor.a = 1f;
                mpcostCorlr.a = 1f;
                skillButtons[i].interactable = true;
                skillname[i].text = skill.SkillName;
                mpcost[i].text = $"MpCost : {skill.MpCost.ToString()}";
            }

            skillname[i].color = nameColor;
            mpcost[i].color = mpcostCorlr;

        }
        for (int i = 0; i < skillButtons.Length; i++)
        {
            int slotIndex = i;
            skillButtons[i].onClick.RemoveAllListeners();
            skillButtons[i].onClick.AddListener(() => OnClickSkillButton(slotIndex));
        }
    }

    private void OnClickSkillButton(int slotIndex)
    {
        PlayerData player = BattleManager.Instance.CurrentPlayer;
        PlayerSkill skill = player.EquippedSkills[slotIndex];
        if (skill == null) return;

        BattleManager.Instance.OnPlayerUseSkill(skill);
    }
}