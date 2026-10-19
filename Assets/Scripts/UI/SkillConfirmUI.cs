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

    private GameObject runtimeRoot;

    // 씬에 배치된 UI가 있으면 그것을, 없으면(상점 등) 런타임으로 만든다
    public static SkillConfirmUI Get()
    {
        if (Instance != null && (Instance.panel != null || Instance.runtimeRoot != null)) return Instance;
        var go = new GameObject("SkillConfirmUI(Runtime)");
        var ui = go.AddComponent<SkillConfirmUI>();
        ui.BuildRuntime();
        Instance = ui;
        return ui;
    }

    public static bool Pending => Instance != null && Instance.IsLevelUp;

    public bool IsLevelUp =>
        (panel != null && panel.activeSelf) || (runtimeRoot != null && runtimeRoot.activeSelf);

    private void BuildRuntime()
    {
        RuntimeUI.EnsureEventSystem();
        Canvas c = RuntimeUI.CreateCanvas("SkillConfirmCanvas", 500);
        c.transform.SetParent(transform, false);
        runtimeRoot = c.gameObject;
        runtimeRoot.SetActive(false);
    }

    private void ShowRuntime(PlayerSkill newSkill)
    {
        foreach (Transform t in runtimeRoot.transform) Destroy(t.gameObject);
        runtimeRoot.SetActive(true);
        Transform root = runtimeRoot.transform;
        RuntimeUI.Panel(root, new Vector2(.5f, .5f), Vector2.zero, new Vector2(4000, 4000), new Color(0, 0, 0, .6f));
        RuntimeUI.Panel(root, new Vector2(.5f, .5f), Vector2.zero, new Vector2(900, 620), new Color(.12f, .12f, .16f, 1f));
        RuntimeUI.Label(root, "'" + newSkill.SkillName + "' 을(를) 어느 슬롯에 장착할까요?",
            new Vector2(.5f, .5f), new Vector2(0, 220), new Vector2(860, 80), 30);

        PlayerData player = GameManager.Instance.CurrentPlayer;
        for (int i = 0; i < player.EquippedSkills.Length; i++)
        {
            int idx = i;
            PlayerSkill cur = player.EquippedSkills[i];
            string n = (i + 1) + ". " + (cur != null ? cur.SkillName : "비어 있음");
            RuntimeUI.Button(root, n, new Vector2(.5f, .5f), new Vector2(0, 100 - i * 90),
                new Vector2(600, 75), new Color(.25f, .35f, .55f), () => OnSelectSlot(idx));
        }
        RuntimeUI.Button(root, "취소", new Vector2(.5f, .5f), new Vector2(0, -265),
            new Vector2(300, 70), new Color(.5f, .25f, .25f), OnCancel);
        pendingSkill = newSkill;
    }

    private void Close()
    {
        if (panel != null) panel.SetActive(false);
        if (runtimeRoot != null) runtimeRoot.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
        if (panel == null) return; // 런타임 생성본
        panel.SetActive(false);
        cancelButton.onClick.AddListener(OnCancel);
    }

    public void ShowReplaceConfirm(PlayerSkill newSkill)
    {
        pendingSkill = newSkill;
        if (panel == null) { ShowRuntime(newSkill); return; }
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
        Close();
        onComplete?.Invoke();
        onComplete = null;
    }

    private void OnCancel()
    {
        Close();
        onComplete?.Invoke();
        onComplete = null;
    }

    public void SetOnComplete(Action callback)
    {
        onComplete = callback;
    }
}