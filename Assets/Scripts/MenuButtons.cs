using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject lankingPanel;

    public void OnGameStartButton()
    {
        GameManager.Instance.StartGame();
    }

    public void OnLankingButton()
    {
        lankingPanel.SetActive(true);
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
