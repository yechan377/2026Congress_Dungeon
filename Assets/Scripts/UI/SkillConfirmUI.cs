using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillConfirmUI : MonoBehaviour
{
    public static SkillConfirmUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private Button cancelButton;

    private PlayerSkill pendingSkill;
    private Action onComplete;

    public bool IsLevelUp => panel.activeSelf;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        cancelButton.onClick.AddListener(OnCancel);
    }

    public void ShowReplaceConfirm(PlayerSkill newSkill)
    {
        pendingSkill = newSkill;
        panel.SetActive(true);
        skillNameText.text = newSkill.SkillName;

        PlayerData player = GameManager.Instance.CurrentPlayer;
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int slotIndex = i;
            PlayerSkill current = player.EquippedSkills[i];

            slotButtons[i].GetComponentInChildren<TMP_Text>().text = current != null ? current.SkillName : "Empty";
            slotButtons[i].onClick.RemoveAllListeners();
            slotButtons[i].onClick.AddListener(() => OnSelectSlot(slotIndex));
        }
    }

    private void OnSelectSlot(int slotIndex)
    {
        GameManager.Instance.CurrentPlayer.EquipSkill(slotIndex, pendingSkill);
        panel.SetActive(false);
        onComplete?.Invoke();
        onComplete = null;
    }

    private void OnCancel()
    {
        panel.SetActive(false);
        onComplete?.Invoke();
        onComplete = null;
    }

    public void SetOnComplete(Action callback)
    {
        onComplete = callback;
    }
}