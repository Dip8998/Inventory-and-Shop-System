using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;

    [SerializeField] private ItemListSO itemList;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private UIService uiService;

    public void SetController(ShopController controller)
    {
        shopController = controller;

        Debug.Log("SetController called in ShopView");
    }

    public void InjectDependencies(ItemView prefab, UIService ui, ItemListSO itemListSO)
    {
        itemViewPrefab = prefab;
        uiService = ui;
        itemList = itemListSO;
    }

    public void OnShowAllItemsButtonClicked()
    {
        shopController.ShowAllItems();
    }

    public void OnShowWeaponItemsButtonClicked()
    {
        shopController.ShowAllItems(ItemType.Weapons);
    }

    public void OnShowConsumableItemsButtonClicked()
    {
        shopController.ShowAllItems(ItemType.Consumables);
    }

    public void OnShowMaterialItemsButtonClicked()
    {
        shopController.ShowAllItems(ItemType.Materials);
    }

    public void OnShowTreasureItemsButtonClicked()
    {
        shopController.ShowAllItems(ItemType.Treasures);
    }

    public ItemListSO GetInventorySO() => itemList;
    public Transform GetItemContainer() => itemContainer;
    public ItemView GetItemViewPrefab() => itemViewPrefab;
    public UIService GetUIService() => uiService;
}
