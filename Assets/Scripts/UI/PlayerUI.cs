using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text HpText;
    [SerializeField] private TMP_Text Mp_Text;
    
    public void UpdateUI(PlayerData data)
    {   
        HpText.text = $"HP : {data.MaxHP}/{data.HP}";
        Mp_Text.text = $"MP : {data.MaxMP}/{data.MP}";
    }
}
