using UnityEngine;
using UnityEngine.UI;

public class RightSideUI : MonoBehaviour
{
    public static RightSideUI Instance;

    [SerializeField] private Button armorbutton;
    [SerializeField] private Button weaponbutton;

    private PlayerData player;

    private void Awake()
    {
        Instance = this;
        player = GameManager.Instance.CurrentPlayer;
        armorbutton.image.sprite = player.EquippedArmor?.Icon;
        weaponbutton.image.sprite = player.EquippedWeapon?.Icon;
    }

    public void EquipArmor()
    {
        armorbutton.image.sprite = player.EquippedArmor.Icon;
    }

    public void UnequipArmor()
    {
        armorbutton.image.sprite = null;
    }

    public void EquipWeapon()
    {
        weaponbutton.image.sprite = player.EquippedWeapon.Icon;
    }

    public void UnequipWeapon()
    {
        weaponbutton.image.sprite = null;
    }
}
