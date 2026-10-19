using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject lankingPanel;

    public void OnGameStartButton()
    {
        GameManager.Instance.StartGame();
    }

    public void OnLankingButton()
    {
        RankingUI.ShowBoard();
    }

    public void OnXButton()
    {
        lankingPanel.SetActive(false);
    }

    public void OnQuitGameButton()
    {
        GameManager.Instance.QuitGame();
    }
}
