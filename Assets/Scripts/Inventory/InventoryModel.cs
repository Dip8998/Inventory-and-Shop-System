using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    private InventoryController inventoryController;
    private List<ItemModel> items = new List<ItemModel>();

    public List<ItemModel> GetItems() => items;

    public void AddItem(ItemSO itemSO)
    {
        items.Add(new ItemModel(itemSO));
        inventoryController.UpdateInventoryView();
    }

    public void SetInventoryController(InventoryController controller)
    {
        inventoryController = controller;
    }
}