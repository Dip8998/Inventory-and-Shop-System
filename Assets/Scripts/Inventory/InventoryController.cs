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
    }
}