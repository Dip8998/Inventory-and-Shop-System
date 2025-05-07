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

    public void GatherResources()
    {
        AddItem();
    }

    public void UpdateInventoryView()
    {
        inventoryView.UpdateInventoryUI(inventoryModel.GetItems());
    }

    public void AddItem()
    {
        int maxItemGenerate = Random.Range(1, 6);
        for(int i = 0; i < maxItemGenerate; i++)
        {
            int randomIndex = Random.Range(0, allItemsList.items.Count);

            ItemSO randomItemSO = allItemsList.items[randomIndex];
            ItemModel gatheredItem = new ItemModel(randomItemSO); 
            inventoryModel.GetItems().Add(gatheredItem);


            int tempWeight = gatheredItem.GetItem().itemQuantity * gatheredItem.GetItem().itemWeight;
            if (GetMaxInventoryWeight() >= GetTotalWeight())
            {
                IncreaseTotalWeight(tempWeight);
                UpdateInventoryView();
            }
            else
            {
                break;
            }
        }
    }

    public int GetMaxInventoryWeight()
    {
        return inventoryModel.GetMaxWeight();
    }

    public int GetTotalWeight()
    {
        return inventoryModel.GetTotalWeight();
    }

    public void IncreaseTotalWeight(int _totalWeight)
    {
        inventoryModel.IncreaseTotalWeight(_totalWeight);
    }
}