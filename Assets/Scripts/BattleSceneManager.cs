using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private EnemyStat testEnemyStat;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        PlayerData player = new PlayerData();
        EnemyData enemy = EnemyFactory.Create(testEnemyStat, level: 1);

        GameObject enemyObj = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        enemyObj.GetComponent<EnemyView>().Setup(enemy);
        playerObj.GetComponent<PlayerView>().Setup(player);

        BattleManager.Instance.StartBattle(player, enemy);
    }
}