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
        EventService.Instance.OnItemTypeButtonClickedEvent.InvokeEvent(null);
    }

    public void OnShowWeaponItemsButtonClicked()
    {
        currentFilter = ItemType.Weapons;
        EventService.Instance.OnItemTypeButtonClickedEvent.InvokeEvent(ItemType.Weapons);
    }

    public void OnShowConsumableItemsButtonClicked()
    {
        currentFilter = ItemType.Consumables;
        EventService.Instance.OnItemTypeButtonClickedEvent.InvokeEvent(ItemType.Consumables);
    }

    public void OnShowMaterialItemsButtonClicked()
    {
        currentFilter = ItemType.Materials;
        EventService.Instance.OnItemTypeButtonClickedEvent.InvokeEvent(ItemType.Materials);
    }

    public void OnShowTreasureItemsButtonClicked()
    {
        currentFilter = ItemType.Treasures;
        EventService.Instance.OnItemTypeButtonClickedEvent.InvokeEvent(ItemType.Treasures);
    }

    public ItemListSO GetInventorySO() => itemList;
    public Transform GetItemContainer() => itemContainer;
    public ItemView GetItemViewPrefab() => itemViewPrefab;
    public UIService GetUIService() => uiService;
    public ItemType? GetCurrentFilter() => currentFilter;
}