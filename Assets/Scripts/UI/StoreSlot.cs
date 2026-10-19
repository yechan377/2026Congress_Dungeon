using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button ItemIconButton;
    [SerializeField] private Button buyButton;

    private object item;
    private int price;

    private void Awake()
    {
        ItemIconButton.onClick.RemoveAllListeners();
        buyButton.onClick.RemoveAllListeners();
        ItemIconButton.onClick.AddListener(OnClickIcon);
        buyButton.onClick.AddListener(OnClickBuy);
    }

    public void SetItem(object itemData, int itemPrice, bool alreadyOwned)
    {
        item = itemData;
        price = itemPrice;

        ItemIconButton.image.sprite = GetIcon(itemData);

        if (alreadyOwned)
        {
            priceText.text = "Sold";
            buyButton.interactable = false;
        }
        else
        {
            priceText.text = $"Price : {price}";
            buyButton.interactable = true;
        }
    }

    private Sprite GetIcon(object item)
    {
        return item switch
        {
            Weapon weapon => weapon.Icon,
            Armor armor => armor.Icon,
            Skill skill => skill.Icon,
            Potion potion => potion.Icon,
            _ => null
        };
    }

    public void Clear()
    {
        item = null;
        ItemIconButton.image.sprite = null;
        priceText.text = "";
        buyButton.interactable = false;
    }

    private void OnClickIcon()
    {
        if (item == null) return;
        IntemInfoUI.Instance.Show(item);
    }

    private void OnClickBuy()
    {
        if (item == null) return;
        ShopManager.Instance.TryBuy(item, price, buyButton);
    }
}