using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private object item;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetItem(object itemData)
    {
        item = itemData;
    }

    public void Clear()
    {
        item = null;
    }

    private void OnClick()
    {
        if (item == null) return;
        IntemInfoUI.Instance.Show(item, transform.position);
    }
}