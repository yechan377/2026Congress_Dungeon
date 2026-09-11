using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private object item;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetItem(object itemData)
    {
        item = itemData;

        if (item != null)
        {
            button.image.sprite = GetIcon(item);
        }
    }

    private Sprite GetIcon(object item)
    {
        return item switch
        {
            Weapon weapon => weapon.Icon,
            Armor armor => armor.Icon,
            PlayerSkill skill => skill.Icon,
            Potion potion => potion.Icon,
            _ => null
        };
    }

    public void Clear()
    {
        item = null;
        button.image.sprite = null;
    }

    private void OnClick()
    {
        if (item == null) return;
        IntemInfoUI.Instance.Show(item, transform.position);
    }
}