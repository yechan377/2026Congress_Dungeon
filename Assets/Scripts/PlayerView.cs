using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    private PlayerData data;
    private PlayerUI playerUI;

    public void Setup(PlayerData playerData)
    {
        data = playerData;
        playerUI = FindFirstObjectByType<PlayerUI>();
        RefreshUI();
        playerUI.UpdateUI(playerData);
    }

    public void RefreshUI()
    {
        if (data == null) return;
        hpBar.maxValue = data.MaxHP;
        hpBar.value = data.HP;
        hpText.text = $"{data.MaxHP}/{data.HP}";
    }
}