using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SceneSelectScene에 자동 생성되는 지도 화면 (GameManager가 만들어줌)
public class MapView : MonoBehaviour
{
    private void Start()
    {
        var gm = GameManager.Instance;
        if (gm.CurrentPlayer == null) gm.StartNewRun(false);   // 씬을 직접 실행했을 때 대비
        RuntimeUI.EnsureEventSystem();
        Build(gm);
    }

    private static int TopIndex(Transform t, Transform canvas)
    {
        while (t.parent != null && t.parent != canvas) t = t.parent;
        return t.GetSiblingIndex();
    }

    private static Color TypeColor(NodeType t) => t switch
    {
        NodeType.Battle => new Color(0.75f, 0.25f, 0.25f),
        NodeType.Shop => new Color(0.25f, 0.65f, 0.35f),
        NodeType.Elite => new Color(0.6f, 0.3f, 0.8f),
        _ => new Color(0.45f, 0.05f, 0.05f),
    };

    private static string TypeName(NodeType t) => t switch
    {
        NodeType.Battle => "전투",
        NodeType.Shop => "상점\n휴식",
        NodeType.Elite => "정예",
        _ => "보스",
    };

    private void Build(GameManager gm)
    {
        // 씬에 원래 있는 Canvas 안에 지도를 만든다 (인벤토리/정보창은 원래 자리 그대로 위에 그려짐)
        var sceneCanvas = FindFirstObjectByType<Canvas>();
        if (sceneCanvas == null) sceneCanvas = RuntimeUI.CreateCanvas("MapCanvas", 0);
        sceneCanvas = sceneCanvas.rootCanvas;

        var rootRect = RuntimeUI.Rect(sceneCanvas.transform, "MapRoot", new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
        rootRect.anchorMin = Vector2.zero; rootRect.anchorMax = Vector2.one;
        rootRect.offsetMin = Vector2.zero; rootRect.offsetMax = Vector2.zero;
        var root = rootRect.transform;

        // 예전 임시 지도 UI(SceneSelect)는 새 지도로 대체되므로 숨김
        var oldMap = sceneCanvas.transform.Find("SceneSelect");
        if (oldMap != null) oldMap.gameObject.SetActive(false);
        var center = new Vector2(0.5f, 0.5f);

        var bg = RuntimeUI.Panel(root, center, Vector2.zero, new Vector2(4000, 3000), new Color(0.08f, 0.08f, 0.12f, 1f));

        RuntimeUI.Label(root, $"STAGE {gm.CurrentStage}  지도", new Vector2(0.5f, 1f), new Vector2(0, -60), new Vector2(800, 80), 48);

        var p = gm.CurrentPlayer;
        RuntimeUI.Label(root, $"Lv.{p.Level}   HP {p.HP}/{p.MaxHP}   MP {p.MP}/{p.MaxMP}   EXP {p.Exp}/{p.GetExpToNextLevel()}   Gold {p.Gold}",
            new Vector2(0.5f, 1f), new Vector2(0, -125), new Vector2(1400, 50), 30);

        var map = gm.CurrentMap;
        var available = map.GetAvailable();
        int layers = map.Layers.Count;

        // 노드 좌표 계산
        Dictionary<MapNodeData, Vector2> pos = new();
        float x0 = -700f, x1 = 700f;
        for (int l = 0; l < layers; l++)
        {
            var layer = map.Layers[l];
            float x = Mathf.Lerp(x0, x1, (float)l / (layers - 1));
            for (int i = 0; i < layer.Count; i++)
            {
                float y = (i - (layer.Count - 1) / 2f) * -220f - 20f;
                pos[layer[i]] = new Vector2(x, y);
            }
        }

        // 선 (먼저 그려서 노드 뒤로)
        foreach (var layer in map.Layers)
            foreach (var n in layer)
                foreach (var nx in n.Next)
                {
                    bool active = n == map.Current && available.Contains(nx);
                    RuntimeUI.Line(root, pos[n], pos[nx], active ? new Color(1f, 0.9f, 0.4f) : new Color(0.4f, 0.4f, 0.45f), active ? 10f : 5f);
                }

        // 노드
        foreach (var layer in map.Layers)
            foreach (var n in layer)
            {
                bool canGo = available.Contains(n);
                Color c = TypeColor(n.Type);
                if (n.Visited) c = new Color(0.3f, 0.3f, 0.3f);
                else if (!canGo) c = c * 0.45f;
                c.a = 1f;

                var node = n;
                var b = RuntimeUI.Button(root, TypeName(n.Type), center, pos[n], n.Type == NodeType.Boss ? new Vector2(170, 170) : new Vector2(130, 130), c, () => gm.SelectNode(node), 26);
                b.interactable = canGo;

                if (canGo) RuntimeUI.Label(b.transform, "▼ 이동 가능", center, new Vector2(0, 100), new Vector2(240, 40), 22, TextAnchor.MiddleCenter, new Color(1f, 0.9f, 0.4f));
                if (n == map.Current) RuntimeUI.Label(b.transform, "현재 위치", center, new Vector2(0, -95), new Vector2(240, 40), 22, TextAnchor.MiddleCenter, Color.white);
            }

        // 인벤토리/아이템 정보창을 지도 위로 올리고, 열기 버튼 추가
        var inv = FindFirstObjectByType<InventoryUI>(FindObjectsInactive.Include);
        var info = FindFirstObjectByType<IntemInfoUI>(FindObjectsInactive.Include);
        if (info != null) info.Prepare();
        if (inv != null)
        {
            inv.Prepare();
            RuntimeUI.Button(root, "인벤토리 (I)", new Vector2(1f, 1f), new Vector2(-150, -60), new Vector2(240, 70), new Color(0.25f, 0.35f, 0.6f), () => inv.ToggleInventory(), 26);
        }

        // 지도는 인벤토리/정보창보다 아래에 그려지도록 순서 조정
        int idx = rootRect.GetSiblingIndex();
        int min = int.MaxValue;
        if (inv != null) min = Mathf.Min(min, TopIndex(inv.transform, sceneCanvas.transform));
        if (info != null) min = Mathf.Min(min, TopIndex(info.transform, sceneCanvas.transform));
        if (min != int.MaxValue) rootRect.SetSiblingIndex(min);

        RuntimeUI.Label(root, "빨강: 전투   초록: 상점·휴식   보라: 정예   검붉음: 보스", new Vector2(0.5f, 0f), new Vector2(0, 50), new Vector2(1400, 50), 26);
    }
}
