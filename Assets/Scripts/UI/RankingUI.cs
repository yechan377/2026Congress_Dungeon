using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable] public class RankEntry { public string name; public float time; }
[Serializable] class RankSave { public List<RankEntry> list = new(); }

// 저장 (상위 5명, 클리어 시간 오름차순)
public static class RankingData
{
    const string KEY = "DungeonRanking";
    public const int MAX = 5;

    public static List<RankEntry> Load()
    {
        try { return JsonUtility.FromJson<RankSave>(PlayerPrefs.GetString(KEY, "{}"))?.list ?? new List<RankEntry>(); }
        catch { return new List<RankEntry>(); }
    }

    public static void Add(string name, float time)
    {
        var l = Load();
        l.Add(new RankEntry { name = name, time = time });
        l = l.OrderBy(e => e.time).Take(MAX).ToList();
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(new RankSave { list = l }));
        PlayerPrefs.Save();
    }

    public static string Format(float t)
    {
        int s = Mathf.RoundToInt(t);
        return $"{s / 60:00}:{s % 60:00}";
    }
}

// 런타임 UI: 클리어 이니셜 입력 / 랭킹판 / 게임오버
public class RankingUI : MonoBehaviour
{
    Canvas canvas;
    string initials = "";
    Text initialsText;
    Button doneBtn;
    float clearTime;

    static RankingUI Make()
    {
        RuntimeUI.EnsureEventSystem();
        var go = new GameObject("RankingUI");
        var ui = go.AddComponent<RankingUI>();
        ui.canvas = RuntimeUI.CreateCanvas("RankingCanvas", 30000);
        ui.canvas.transform.SetParent(go.transform, false);
        RuntimeUI.Panel(ui.canvas.transform, new Vector2(.5f, .5f), Vector2.zero, new Vector2(4000, 4000), new Color(0, 0, 0, .96f));
        return ui;
    }

    // 게임 클리어 -> 이니셜 입력
    public static void ShowClear(float time)
    {
        var ui = Make(); ui.clearTime = time; ui.BuildInput();
    }

    public static void ShowBoard()
    {
        var ui = Make(); ui.BuildBoard(false);
    }

    public static void ShowGameOver()
    {
        var ui = Make(); ui.BuildGameOver();
    }

    void BuildInput()
    {
        Transform t = canvas.transform;
        Vector2 c = new Vector2(.5f, .5f);
        RuntimeUI.Label(t, "게임 클리어!", c, new Vector2(0, 450), new Vector2(900, 90), 64, TextAnchor.MiddleCenter, Color.yellow);
        RuntimeUI.Label(t, "클리어 시간  " + RankingData.Format(clearTime), c, new Vector2(0, 360), new Vector2(900, 60), 40);
        RuntimeUI.Label(t, "이니셜을 입력하세요 (2~3글자)", c, new Vector2(0, 290), new Vector2(900, 50), 30);
        initialsText = RuntimeUI.Label(t, "_ _ _", c, new Vector2(0, 210), new Vector2(600, 90), 70, TextAnchor.MiddleCenter, Color.cyan);

        // A-Z 버튼
        for (int i = 0; i < 26; i++)
        {
            string ch = ((char)('A' + i)).ToString();
            int row = i / 9, col = i % 9;
            RuntimeUI.Button(t, ch, c, new Vector2((col - 4) * 90, 100 - row * 90), new Vector2(80, 80), new Color(.25f, .3f, .45f), () => AddLetter(ch), 34);
        }
        RuntimeUI.Button(t, "←", c, new Vector2(4 * 90, 100 - 2 * 90), new Vector2(80, 80), new Color(.5f, .3f, .2f), Back, 34);
        doneBtn = RuntimeUI.Button(t, "완료", c, new Vector2(0, -220), new Vector2(360, 90), new Color(.2f, .55f, .3f), Done, 40);
        Refresh();
    }

    void AddLetter(string ch) { if (initials.Length < 3) { initials += ch; Refresh(); } }
    void Back() { if (initials.Length > 0) { initials = initials.Substring(0, initials.Length - 1); Refresh(); } }

    void Refresh()
    {
        string s = "";
        for (int i = 0; i < 3; i++) s += (i < initials.Length ? initials[i].ToString() : "_") + (i < 2 ? " " : "");
        initialsText.text = s;
        doneBtn.interactable = initials.Length >= 2;
        doneBtn.targetGraphic.color = doneBtn.interactable ? new Color(.2f, .55f, .3f) : new Color(.3f, .3f, .3f);
    }

    void Done()
    {
        if (initials.Length < 2) return;
        RankingData.Add(initials, clearTime);
        foreach (Transform ch in canvas.transform) if (ch.name != "Panel") Destroy(ch.gameObject);
        BuildBoard(true);
    }

    void BuildBoard(bool toMenu)
    {
        Transform t = canvas.transform;
        Vector2 c = new Vector2(.5f, .5f);
        RuntimeUI.Label(t, "랭킹 TOP 5", c, new Vector2(0, 330), new Vector2(900, 90), 60, TextAnchor.MiddleCenter, Color.yellow);
        var list = RankingData.Load();
        for (int i = 0; i < RankingData.MAX; i++)
        {
            string line = i < list.Count ? $"{i + 1}.   {list[i].name,-3}      {RankingData.Format(list[i].time)}" : $"{i + 1}.   ---";
            RuntimeUI.Label(t, line, c, new Vector2(0, 200 - i * 80), new Vector2(700, 70), 44, TextAnchor.MiddleLeft);
        }
        RuntimeUI.Button(t, toMenu ? "메인 메뉴" : "닫기", c, new Vector2(0, -280), new Vector2(360, 90), new Color(.3f, .35f, .55f), () =>
        {
            if (toMenu) SceneManager.LoadScene("MenuScene");
            Destroy(gameObject);
        }, 40);
    }

    void BuildGameOver()
    {
        Transform t = canvas.transform;
        Vector2 c = new Vector2(.5f, .5f);
        RuntimeUI.Label(t, "GAME OVER", c, new Vector2(0, 150), new Vector2(900, 120), 90, TextAnchor.MiddleCenter, new Color(.9f, .2f, .2f));
        RuntimeUI.Button(t, "메인 메뉴", c, new Vector2(0, -80), new Vector2(400, 100), new Color(.3f, .35f, .55f), () => SceneManager.LoadScene("MenuScene"), 44);
    }
}
