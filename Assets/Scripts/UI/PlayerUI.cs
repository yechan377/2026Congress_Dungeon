using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text HpText;
    [SerializeField] private TMP_Text Mp_Text;
    [SerializeField] private TMP_Text Gold_Text;
    [SerializeField] private TMP_Text Level_Text;
    [SerializeField] private Slider Exp_Bar;
    private PlayerData data;
    public void Setup(PlayerData playerData)
    {
        data = playerData;
        data.OnStatChanged += RefreshUI;
        RefreshUI(data);
    }
    private void RefreshUI(Character character)
    {
        if (data == null) return;
        HpText.text = $"HP : {data.HP}/{data.MaxHP}";
        Mp_Text.text = $"MP : {data.MP}/{data.MaxMP}";
        Gold_Text.text = $"Gold : {data.Gold}";
        Level_Text.text = $"{data.Level}";
        Exp_Bar.maxValue = data.GetExpToNextLevel();
        Exp_Bar.value = data.Exp;
    }

    private void OnDestroy()
    {
        data.OnStatChanged -= RefreshUI;
    }
}