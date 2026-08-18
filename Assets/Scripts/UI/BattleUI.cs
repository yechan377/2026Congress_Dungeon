using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private Button[] skillButtons;

    private void Start()
    {
        foreach (Button btn in skillButtons)
        {
            btn.onClick.AddListener(() => OnClickSkillButton());
        }
    }

    private void OnClickSkillButton()
    {
        BattleManager.Instance.Attack(BattleManager.Instance.CurrentEnemy, 10);
    }

    public void SetInteractable(bool enable)
    {
        foreach (Button btn in skillButtons)
        {
            btn.interactable = enable;
        }
    }
}