using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    private EnemyData data;

    private void OnEnable()
    {
        EventManager.Instance.AddListener(EventType.OnHpChanged, OnHpChanged);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(EventType.OnHpChanged, OnHpChanged);
    }

    private void OnHpChanged(EventType type, Component sender, params object[] args)
    {
        Character changed = (Character)args[0];
        if (changed != data) return;

        RefreshUI();
    }

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
        hpText.text = $"{data.HP}/{data.MaxHP}";
    }
}