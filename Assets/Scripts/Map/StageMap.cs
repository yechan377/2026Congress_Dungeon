using System.Collections.Generic;
using UnityEngine;

public enum NodeType { Battle, Shop, Elite, Boss }

public class MapNodeData
{
    public int Layer;
    public int Index;
    public NodeType Type;
    public bool Visited;
    public List<MapNodeData> Next = new();
}

public class StageMap
{
    public List<List<MapNodeData>> Layers = new();
    public MapNodeData Current;   // null이면 아직 출발 전

    private const int NORMAL_LAYERS = 5;

    public static StageMap Generate(int stage)
    {
        StageMap map = new();

        for (int l = 0; l < NORMAL_LAYERS; l++)
        {
            int count = Random.Range(2, 4);
            List<MapNodeData> layer = new();
            for (int i = 0; i < count; i++)
            {
                layer.Add(new MapNodeData { Layer = l, Index = i, Type = RollType(l) });
            }
            map.Layers.Add(layer);
        }
        map.Layers.Add(new List<MapNodeData> { new MapNodeData { Layer = NORMAL_LAYERS, Index = 0, Type = NodeType.Boss } });

        // 3스테이지만 정예 1개 (2~3번째 층 중 하나)
        if (stage >= 3)
        {
            var layer = map.Layers[Random.Range(2, 4)];
            layer[Random.Range(0, layer.Count)].Type = NodeType.Elite;
        }

        // 상점이 하나도 없으면 3번째 층에 보장
        bool hasShop = false;
        foreach (var layer in map.Layers) foreach (var n in layer) if (n.Type == NodeType.Shop) hasShop = true;
        if (!hasShop)
        {
            var layer = map.Layers[2];
            var n = layer[Random.Range(0, layer.Count)];
            if (n.Type != NodeType.Elite) n.Type = NodeType.Shop;
        }

        map.Connect();
        return map;
    }

    private static NodeType RollType(int layer)
    {
        if (layer == 0) return NodeType.Battle;
        return Random.value < 0.3f ? NodeType.Shop : NodeType.Battle;
    }

    private void Connect()
    {
        for (int l = 0; l < Layers.Count - 1; l++)
        {
            var from = Layers[l];
            var to = Layers[l + 1];

            // 각 노드는 비슷한 위치의 다음 노드로 연결, 가끔 하나 더
            foreach (var n in from)
            {
                int t = Mathf.Clamp(Mathf.RoundToInt((float)n.Index / Mathf.Max(1, from.Count - 1) * (to.Count - 1)), 0, to.Count - 1);
                AddEdge(n, to[t]);
                if (to.Count > 1 && Random.value < 0.4f)
                {
                    int t2 = Mathf.Clamp(t + (Random.value < 0.5f ? -1 : 1), 0, to.Count - 1);
                    AddEdge(n, to[t2]);
                }
            }

            // 들어오는 길이 없는 노드는 가까운 노드에서 연결
            foreach (var m in to)
            {
                bool hasIn = false;
                foreach (var n in from) if (n.Next.Contains(m)) hasIn = true;
                if (hasIn) continue;
                int f = Mathf.Clamp(Mathf.RoundToInt((float)m.Index / Mathf.Max(1, to.Count - 1) * (from.Count - 1)), 0, from.Count - 1);
                AddEdge(from[f], m);
            }
        }
    }

    private void AddEdge(MapNodeData a, MapNodeData b)
    {
        if (!a.Next.Contains(b)) a.Next.Add(b);
    }

    public List<MapNodeData> GetAvailable()
    {
        return Current == null ? Layers[0] : Current.Next;
    }
}
