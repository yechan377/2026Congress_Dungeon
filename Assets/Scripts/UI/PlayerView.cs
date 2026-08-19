using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;
    private PlayerData data;
    public void Setup(PlayerData playerData)
    {
        data = playerData;
        data.OnStatChanged += RefreshUI;
        RefreshUI(data);
    }

    private void OnDestroy()
    {
        if (data == null) return;
        data.OnStatChanged -= RefreshUI;
    }

    public void RefreshUI(Character character)
    {
        if (data == null) return;
        hpBar.maxValue = data.MaxHP;
        hpBar.value = data.HP;
        hpText.text = $"{data.HP}/{data.MaxHP}";
    }
}