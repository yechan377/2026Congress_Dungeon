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
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (data == null) return;
        hpBar.maxValue = data.MaxHP;
        hpBar.value = data.HP;
        hpText.text = $"{data.MaxHP}/{data.HP}";
    }
}