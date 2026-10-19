using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// 개발용 치트키 (F1~F10). GameManager가 생성.
public class CheatKeys : MonoBehaviour
{
    public static bool Invincible;
    public static int AttackBonus;

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;
        var gm = GameManager.Instance;
        var p = gm != null ? gm.CurrentPlayer : null;

        if (kb.f1Key.wasPressedThisFrame) { Invincible = !Invincible; Debug.Log("무적 " + Invincible); }
        if (p != null)
        {
            if (kb.f2Key.wasPressedThisFrame) { p.Damage += 100; Debug.Log("공격력 +100"); }
            if (kb.f3Key.wasPressedThisFrame) { p.HP = p.MaxHP; p.OnStatChanged?.Invoke(p); }
            if (kb.f4Key.wasPressedThisFrame) { p.FillMp(p.MaxMP); }
            if (kb.f5Key.wasPressedThisFrame) { p.AddExp(p.GetExpToNextLevel() - p.Exp); }
            if (Invincible && p.HP < p.MaxHP) { p.HP = p.MaxHP; p.OnStatChanged?.Invoke(p); }
        }
        if (kb.f6Key.wasPressedThisFrame && BattleManager.Instance != null)
        {
            foreach (var e in BattleManager.Instance.GetAliveEnemies()) { e.HP = 0; e.OnStatChanged?.Invoke(e); }
            BattleManager.Instance.CheckBattleEnd();
        }
        if (kb.f7Key.wasPressedThisFrame) SceneManager.LoadScene("MenuScene");
        if (p != null)
        {
            if (kb.f8Key.wasPressedThisFrame) Jump(1);
            if (kb.f9Key.wasPressedThisFrame) Jump(2);
            if (kb.f10Key.wasPressedThisFrame) Jump(3);
        }
    }

    void Jump(int stage)
    {
        GameManager.Instance.GoToStage(stage);
        GameManager.Instance.SceneSelectScene();
    }
}
