using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManagers<GameManager>
{
    public PlayerData CurrentPlayer { get; private set; }
    public bool IsSceneSelectScene = false;
    public string CurrentNodeId;

    public int CurrentStage { get; private set; } = 1;
    public StageMap CurrentMap { get; private set; }
    public MapNodeData CurrentNode { get; private set; }
    public float RunStartTime { get; private set; }

    private const int LAST_STAGE = 3;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        var _ = Instance;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EnsureMapView(SceneManager.GetActiveScene());
        if (GetComponent<CheatKeys>() == null) gameObject.AddComponent<CheatKeys>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureMapView(scene);
    }

    private void EnsureMapView(Scene scene)
    {
        IsSceneSelectScene = scene.name == "SceneSelectScene";
        if (scene.name != "SceneSelectScene") return;
        if (FindFirstObjectByType<MapView>() != null) return;
        new GameObject("MapView").AddComponent<MapView>();
    }

    public void InitNewGame()
    {
        CurrentPlayer = new PlayerData();
    }

    public void SetPlayer(PlayerData player)
    {
        CurrentPlayer = player;
    }

    // 새 게임 상태 초기화 (스테이지1, 새 지도)
    public void StartNewRun(bool resetTimer = true)
    {
        InitNewGame();
        if (resetTimer) RunStartTime = Time.time;
        GoToStage(1);
    }

    public void GoToStage(int stage)
    {
        CurrentStage = Mathf.Clamp(stage, 1, LAST_STAGE);
        CurrentMap = StageMap.Generate(CurrentStage);
        CurrentNode = null;
    }

    // 게임 시작 -> 무조건 지도 선택 화면
    public void StartGame()
    {
        StartNewRun();
        SceneSelectScene();
    }

    public void SelectNode(MapNodeData node)
    {
        if (!CurrentMap.GetAvailable().Contains(node)) return;

        CurrentMap.Current = node;
        CurrentNode = node;
        node.Visited = true;

        if (node.Type == NodeType.Shop) StoreSceneLoad();
        else SceneManager.LoadScene("StageScene");   // 전투/정예/보스는 같은 전투 씬 재사용
    }

    public NodeType CurrentNodeType => CurrentNode != null ? CurrentNode.Type : NodeType.Battle;

    // 전투 승리 후
    public void OnBattleVictory()
    {
        if (CurrentNodeType == NodeType.Boss) OnStageClear();
        else SceneSelectScene();
    }

    private void OnStageClear()
    {
        if (CurrentStage >= LAST_STAGE)
        {
            RankingUI.ShowClear(Time.time - RunStartTime);
            return;
        }
        GoToStage(CurrentStage + 1);
        SceneSelectScene();
    }

    public void StoreSceneLoad()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void SceneSelectScene()
    {
        SceneManager.LoadScene("SceneSelectScene");
    }

    // 예전 '다음 스테이지' 버튼 호환: 지도로 이동
    public void NextStage()
    {
        SceneSelectScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
