using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void TryBuy(object item, int price, Button buybutton)
    {
        PlayerData player = GameManager.Instance.CurrentPlayer;

        if (player.Gold < price)
        {
            Debug.Log("°ñµå ºÎÁ·");
            return;
        }

        player.SpendGold(price);

        switch (item)
        {
            case Weapon weapon: player.AddWeapon(weapon);  buybutton.interactable = false; break;
            case Armor armor: player.AddArmor(armor); buybutton.interactable = false; break;
            case Potion potion: player.AddPotion(potion, 1); break;
            case PlayerSkill skill: player.LearnedSkills.Add(skill); buybutton.interactable = false; break;
        }
    }
}