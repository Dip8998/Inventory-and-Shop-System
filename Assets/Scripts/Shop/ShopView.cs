using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;

    [SerializeField] private ItemListSO itemList;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private UIService uiService;
    
    private ItemSO selectedItemToBuy;
    private ItemType? currentFilter;

    public void SetController(ShopController controller)
    {
        shopController = controller;
    }

    public void InjectDependencies(ItemView prefab, UIService ui, ItemListSO itemListSO)
    {
        itemViewPrefab = prefab;
        uiService = ui;
        itemList = itemListSO;
    }

    public void OnShowAllItemsButtonClicked()
    {
        currentFilter = null;
        shopController.ShowAllItems();
    }

    public void OnShowWeaponItemsButtonClicked()
    {
        currentFilter = ItemType.Weapons;
        shopController.ShowAllItems(ItemType.Weapons);
    }

    public void OnShowConsumableItemsButtonClicked()
    {
        currentFilter = ItemType.Consumables;
        shopController.ShowAllItems(ItemType.Consumables);
    }

    public void OnShowMaterialItemsButtonClicked()
    {
        currentFilter = ItemType.Materials;
        shopController.ShowAllItems(ItemType.Materials);
    }

    public void OnShowTreasureItemsButtonClicked()
    {
        currentFilter = ItemType.Treasures;
        shopController.ShowAllItems(ItemType.Treasures);
    }

    public ItemListSO GetInventorySO() => itemList;
    public Transform GetItemContainer() => itemContainer;
    public ItemView GetItemViewPrefab() => itemViewPrefab;
    public UIService GetUIService() => uiService;
    public ItemType? GetCurrentFilter() => currentFilter;
}