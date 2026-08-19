using UnityEngine;

public class BattleActionUI : MonoBehaviour
{
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private GameObject potionPanel;

    public void OnClickAttackButton()
    {
        skillPanel.SetActive(true);
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
}
