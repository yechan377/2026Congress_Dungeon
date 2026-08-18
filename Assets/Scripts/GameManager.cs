using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManagers<GameManager>
{
    private void Awake()
    {
        EventManager.Instance.AddListener(EventType.OnEnemyDead, OnEnemyDead);
    }

    private void OnEnemyDead(EventType type, Component sender, params object[] args)
    {
        Debug.Log("½Â¸®");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void QuitGame()
    {
    }
}
