using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextStageButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.gameObject.SetActive(false); // 평소엔 숨김
        button.onClick.AddListener(OnClick);
    }

    public void Show()
    {
        button.gameObject.SetActive(true);
    }

    private void OnClick()
    {
        button.gameObject.SetActive(false);

        var sceneManager = FindFirstObjectByType<BattleSceneManager>();
        List<EnemyData> enemies = sceneManager.SpawnEnemies();
        BattleManager.Instance.StartBattle(GameManager.Instance.CurrentPlayer, enemies); // 완전히 새 전투 시작
    }
}