using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    private ItemListSO allItemsList; 

    public InventoryController(InventoryModel model, InventoryView view, ItemListSO allItems)
    {
        inventoryModel = model;
        inventoryView = view;
        allItemsList = allItems;

        inventoryModel.SetInventoryController(this);
        inventoryView.SetController(this);

        inventoryView.UpdateInventoryUI(inventoryModel.GetItems());
    }

    public void GatherResources()
    {
        if (allItemsList != null && allItemsList.items.Count > 0)
        {
            int randomIndex = Random.Range(0, allItemsList.items.Count);
            ItemSO gatheredItem = allItemsList.items[randomIndex];
            inventoryModel.AddItem(gatheredItem);
        }
        else
        {
            Debug.Log("No items available to gather!");
        }
    }

    public void UpdateInventoryView()
    {
        inventoryView.UpdateInventoryUI(inventoryModel.GetItems());
    }
}