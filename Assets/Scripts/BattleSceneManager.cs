using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private EnemyStat testEnemyStat;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        PlayerData player = GameManager.Instance.CurrentPlayer;
        if (player == null)
        {
            player = new PlayerData();
            GameManager.Instance.SetPlayer(player);
        }

        List<EnemyData> enemies = SpawnEnemies();

        GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerObj.GetComponent<PlayerView>().Setup(player);
        FindFirstObjectByType<PlayerUI>().Setup(player);

        BattleManager.Instance.StartBattle(player, enemies);
    }

    public List<EnemyData> SpawnEnemies()
    {
        Debug.Log("적 생성");
        var gm = GameManager.Instance;
        List<EnemyData> enemies = MonsterTable.Spawn(gm.CurrentStage, gm.CurrentNodeType, enemySpawnPoints.Length);

        for (int i = 0; i < enemies.Count; i++)
        {
            Transform spawnPoint = enemySpawnPoints[i];
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyObj.GetComponent<EnemyView>().Setup(enemies[i]);
        }

        return enemies;
    }

    public void RestartWithNewEnemies()
    {
        ClearEnemies();

        List<EnemyData> enemies = SpawnEnemies();
        BattleManager.Instance.StartBattle(GameManager.Instance.CurrentPlayer, enemies);
        FindFirstObjectByType<BattleUI>().RefreshButtons();
    }

    public void ClearEnemies()
    {
        var enemyViews = FindObjectsByType<EnemyView>(FindObjectsSortMode.None);
        foreach (var enemyView in enemyViews)
        {
            Destroy(enemyView.gameObject);
        }
    }
}