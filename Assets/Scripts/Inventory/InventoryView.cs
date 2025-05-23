using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryController inventoryController;
    [SerializeField] private InventoryManager inventoryManager; 
    [SerializeField] private Transform inventoryItemContainer;
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private UIService uiService;

    public void SetController(InventoryController controller)
    {
        inventoryController = controller;
    }

    public void InjectDependencies(ItemView prefab, UIService ui)
    {
        itemViewPrefab = prefab;
        uiService = ui;
    }

    void Awake()
    {
        EventService.Instance.OnInventoryChangedEvent.AddListener(UpdateInventoryUIInternal);
    }

    void OnDestroy()
    {
        EventService.Instance.OnInventoryChangedEvent.RemoveListener(UpdateInventoryUIInternal);
    }

    public void UpdateInventoryUI(List<ItemModel> items)
    {
        UpdateInventory(items);
        uiService.UpdateWeightText();
    }

    private void UpdateInventoryUIInternal()
    {
        if (inventoryManager != null && inventoryManager.inventoryModel != null)
        {
            UpdateInventoryUI(inventoryManager.inventoryModel.GetItems());
        }
    }

    private void UpdateInventory(List<ItemModel> items)
    {
        ClearAllItems();

        foreach (ItemModel itemModel in items)
        {
            ItemView itemViewInstance = Instantiate(itemViewPrefab, inventoryItemContainer);
            ItemController itemController = new ItemController(itemModel, itemViewInstance, uiService);
            itemViewInstance.SetController(itemController);
        }
    }

    private void ClearAllItems()
    {
        UIHelper.ClearContainerChildren(inventoryItemContainer);
    }

    public UIService GetUIService()
    {
        return uiService;
    }
}