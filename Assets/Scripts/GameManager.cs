using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManagers<GameManager>
{
    public PlayerData CurrentPlayer { get; private set; }

    public void InitNewGame()
    {
        CurrentPlayer = new PlayerData();

        if (CurrentPlayer == null) CurrentPlayer = new PlayerData();
    }

    public void SetPlayer(PlayerData player)
    {
        CurrentPlayer = player;
    }

    public void Victory()
    {
        Debug.Log("½Â¸®");
        SceneManager.LoadScene("MenuScene");
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
