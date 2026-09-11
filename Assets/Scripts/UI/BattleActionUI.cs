using UnityEngine;

public class BattleActionUI : MonoBehaviour
{
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private GameObject potionPanel;
    [SerializeField] private GameObject targetSelectPanel;

    public void OnClickAttackButton()
    {
        skillPanel.SetActive(true);
        FindFirstObjectByType<BattleUI>().RefreshButtons();
    }

    public void OnClickPotionButton()
    {
        potionPanel.SetActive(true);
    }

    public void OnClickXButton()
    {
        skillPanel.SetActive(false);
        potionPanel.SetActive(false);
    }

    public void OnclickTargetXButton()
    {
        targetSelectPanel.SetActive(false);
    }

    public void OnClickPassButton()
    {
        BattleManager.Instance.OnPlayerPassTurn();
    }
}
