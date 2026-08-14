using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManagers<GameManager>
{
    public void StartGame()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void QuitGame()
    {
    }
}
