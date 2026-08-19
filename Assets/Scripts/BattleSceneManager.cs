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
        PlayerData player = GameManager.Instance.CurrentPlayer;
        if (player == null)
        {
            player = new PlayerData();
            GameManager.Instance.SetPlayer(player);
        }
        List<EnemyData> enemies = new() {
            EnemyFactory.Create(testEnemyStat, Random.Range(1, 4)),
            EnemyFactory.Create(testEnemyStat, Random.Range(1, 4))
        };

        GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerObj.GetComponent<PlayerView>().Setup(player);
        FindFirstObjectByType<PlayerUI>().Setup(player);

        for (int i = 0; i < enemies.Count; i++)
        {
            Transform spawnPoint = enemySpawnPoints[i];
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyObj.GetComponent<EnemyView>().Setup(enemies[i]);
        }

        BattleManager.Instance.StartBattle(player, enemies);
    }
}