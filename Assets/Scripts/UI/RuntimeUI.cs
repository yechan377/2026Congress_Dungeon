using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

// 에셋/프리팹 없이 코드로 UI를 만들기 위한 도우미
public static class RuntimeUI
{
    private static Font font;
    public static Font Font
    {
        get
        {
            if (font == null) font = Font.CreateDynamicFontFromOSFont(new[] { "Malgun Gothic", "NanumGothic", "Arial" }, 24);
            return font;
        }
    }

    public static void EnsureEventSystem()
    {
        if (Object.FindFirstObjectByType<EventSystem>() != null) return;
        var go = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
    }

    public static Canvas CreateCanvas(string name, int order)
    {
        var go = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var c = go.GetComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = order;
        var s = go.GetComponent<CanvasScaler>();
        s.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        s.referenceResolution = new Vector2(1920, 1080);
        s.matchWidthOrHeight = 0.5f;
        return c;
    }

    public static RectTransform Rect(Transform parent, string name, Vector2 anchor, Vector2 pos, Vector2 size)
    {
        var go = new GameObject(name, typeof(RectTransform));
        var r = go.GetComponent<RectTransform>();
        r.SetParent(parent, false);
        r.anchorMin = r.anchorMax = anchor;
        r.pivot = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = pos;
        r.sizeDelta = size;
        return r;
    }

    public static Image Panel(Transform parent, Vector2 anchor, Vector2 pos, Vector2 size, Color color)
    {
        var r = Rect(parent, "Panel", anchor, pos, size);
        var img = r.gameObject.AddComponent<Image>();
        img.color = color;
        return img;
    }

    public static Text Label(Transform parent, string text, Vector2 anchor, Vector2 pos, Vector2 size, int fontSize = 28, TextAnchor align = TextAnchor.MiddleCenter, Color? color = null)
    {
        var r = Rect(parent, "Text", anchor, pos, size);
        var t = r.gameObject.AddComponent<Text>();
        t.font = Font; t.text = text; t.fontSize = fontSize; t.alignment = align;
        t.color = color ?? Color.white;
        t.raycastTarget = false;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        return t;
    }

    public static Button Button(Transform parent, string text, Vector2 anchor, Vector2 pos, Vector2 size, Color color, UnityEngine.Events.UnityAction onClick, int fontSize = 26)
    {
        var img = Panel(parent, anchor, pos, size, color);
        img.gameObject.name = "Button";
        var b = img.gameObject.AddComponent<Button>();
        b.targetGraphic = img;
        b.onClick.AddListener(onClick);
        Label(img.transform, text, new Vector2(0.5f, 0.5f), Vector2.zero, size, fontSize);
        return b;
    }

    // 두 점 사이 선
    public static void Line(Transform parent, Vector2 a, Vector2 b, Color color, float width = 6f)
    {
        var r = Rect(parent, "Line", new Vector2(0.5f, 0.5f), (a + b) / 2f, new Vector2(Vector2.Distance(a, b), width));
        r.gameObject.AddComponent<Image>().color = color;
        r.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg);
        r.GetComponent<Image>().raycastTarget = false;
    }
}
