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
        hpBar.maxValue = data.MaxHP;
        hpBar.value = data.HP;
        hpText.text = $"{data.HP}/{data.MaxHP}";
    }
}