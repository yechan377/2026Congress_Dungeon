using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManagers<GameManager>
{
    public PlayerData CurrentPlayer { get; private set; }
    public bool IsSceneSelectScene = false;
    public string CurrentNodeId;
    private int currentStage = 1;

    public void InitNewGame()
    {
        CurrentPlayer = new PlayerData();

        if (CurrentPlayer == null) CurrentPlayer = new PlayerData();
    }

    public void SetPlayer(PlayerData player)
    {
        CurrentPlayer = player;
    }

    public void StoreSceneLoad()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void SceneSelectScene()
    {
        SceneManager.LoadScene("SceneSelectScene");
    }

    public void NextStage()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void StartGame()
    {
        InitNewGame();
        SceneManager.LoadScene("StageScene");
    }

    public void QuitGame()
    {
    }
}
