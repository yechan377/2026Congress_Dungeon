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
        RefreshUI(data);
    }

    private void OnDestroy()
    {
        if (data != null) data.OnStatChanged -= RefreshUI;
    }

    private void RefreshUI(Character character)
    {
        Debug.Log($"{character.MaxHP}/{character.HP}"); // 60/60 로그
        hpBar.maxValue = character.MaxHP;
        hpBar.value = character.HP;
        hpText.text = $"{character.HP}/{character.MaxHP}"; // 화면엔 4/60로 보임
    }
}