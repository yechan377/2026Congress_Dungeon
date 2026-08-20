using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private TMP_Text[] skillTexts;

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

            Image btnImage = skillButtons[i].GetComponent<Image>();
            Color imgColor = btnImage.color;
            Color txtColor = skillTexts[i].color;

            if (skill == null)
            {
                imgColor.a = 0f;
                txtColor.a = 0f;
                skillButtons[i].interactable = false;
            }
            else
            {
                imgColor.a = 1f;
                txtColor.a = 1f;
                skillButtons[i].interactable = true;
                skillTexts[i].text = skill.SkillName;
            }

            btnImage.color = imgColor;
            skillTexts[i].color = txtColor;

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

    public void SetInteractable(bool enable)
    {
        foreach (Button btn in skillButtons)
        {
            Image img = btn.GetComponent<Image>();
            if (img.color.a > 0f)
            {
                btn.interactable = enable;
            }
        }
    }
}