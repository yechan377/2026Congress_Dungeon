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
        // 1. 데이터 준비
        PlayerData player = new PlayerData();
        EnemyData enemy = EnemyFactory.Create(testEnemyStat, level: 1);

        // 2. 프리팹 소환
        GameObject enemyObj = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        // 3. 소환된 오브젝트의 View에 데이터 연결
        enemyObj.GetComponent<EnemyView>().Setup(enemy);
        playerObj.GetComponent<PlayerView>().Setup(player);

        // 4. BattleManager에게 전투 시작 요청 (데이터만 넘김, 프리팹은 View가 알아서 처리)
        BattleManager.Instance.StartBattle(player, enemy);
    }
}