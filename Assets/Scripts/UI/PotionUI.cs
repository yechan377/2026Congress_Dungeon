using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour
{
    [SerializeField] private Button[] potionButtons;
    [SerializeField] private TMP_Text[] potionTexts;
    [SerializeField] private TMP_Text[] descriptionTexts;

    private void Awake()
    {
        RefreshPotions();
    }

    public void RefreshPotions()
    {
        PlayerData player = BattleManager.Instance.CurrentPlayer;
        if (player == null) return;

        int index = 0;
        foreach (var pair in player.Potions)
        {
            if (index >= potionButtons.Length) break;

            Potion potion = pair.Key;
            int count = pair.Value;

            potionTexts[index].text = $"{potion.PotionName} : {count}";
            descriptionTexts[index].text = potion.Description;

            potionButtons[index].onClick.RemoveAllListeners();
            potionButtons[index].onClick.AddListener(() => OnClickPotion(potion));

            index++;
        }
    }

    private void OnClickPotion(Potion potion)
    {
        BattleManager.Instance.CurrentPlayer.TryUsePotion(potion);
        RefreshPotions();
    }
}