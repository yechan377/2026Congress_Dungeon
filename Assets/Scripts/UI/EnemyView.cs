using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    private EnemyData data;

    public void Setup(EnemyData enemyData)
    {
        Debug.Log("SetUp");
        data = enemyData;
        data.OnStatChanged += RefreshUI;
        data.OnIntentChanged += RefreshIntent;
        CreateIntentText();
        RefreshUI(data);
        RefreshIntent(data);
    }

    private void OnDestroy()
    {
        if (data != null)
        {
            data.OnStatChanged -= RefreshUI;
            data.OnIntentChanged -= RefreshIntent;
        }
    }

    private Text intentText;

    // 적 머리 위에 다음 행동 표시 (코드로 생성)
    private void CreateIntentText()
    {
        Canvas cv = hpBar.GetComponentInParent<Canvas>();
        Transform parent = cv != null ? cv.transform : hpBar.transform.parent;
        var go = new GameObject("Intent", typeof(RectTransform));
        var r = go.GetComponent<RectTransform>();
        r.SetParent(parent, false);
        r.anchorMin = r.anchorMax = new Vector2(0.5f, 0.5f);
        r.pivot = new Vector2(0.5f, 0.5f);
        r.sizeDelta = new Vector2(300, 50);
        r.localScale = hpBar.transform.localScale;   // 월드 캔버스: HpBar와 같은 스케일
        r.localPosition = hpBar.transform.localPosition + Vector3.up * 3f;
        r.SetAsLastSibling();
        intentText = go.AddComponent<Text>();
        intentText.font = RuntimeUI.Font;
        intentText.fontSize = 32;
        intentText.alignment = TextAnchor.MiddleCenter;
        intentText.color = Color.yellow;
        intentText.raycastTarget = false;
        intentText.horizontalOverflow = HorizontalWrapMode.Overflow;
    }

    private void RefreshIntent(EnemyData e)
    {
        if (intentText == null) return;
        intentText.text = e.IsDead() ? "" : e.IntentText();
    }

    private void RefreshUI(Character character)
    {
        Debug.Log($"{character.MaxHP}/{character.HP}"); // 60/60 로그
        hpBar.maxValue = character.MaxHP;
        hpBar.value = character.HP;
        hpText.text = $"{character.HP}/{character.MaxHP}"; // 화면엔 4/60로 보임

        // 죽으면 바로 제거
        if (character.IsDead()) Destroy(gameObject);
    }
}