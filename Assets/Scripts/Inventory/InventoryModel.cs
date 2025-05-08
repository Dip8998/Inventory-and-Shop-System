using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    private InventoryController inventoryController;
    private List<ItemModel> items = new List<ItemModel>();
    private int maxWeight = 200;
    private int totalWeight;

    public List<ItemModel> GetItems() => items;

    public void SetInventoryController(InventoryController controller)
    {
        inventoryController = controller;
    }

    public int GetMaxWeight()
    {
        return maxWeight;
    }

    public int GetTotalWeight()
    {
        return totalWeight;
    }

    public void IncreaseTotalWeight(int additionalWeight)
    {
        totalWeight += additionalWeight;
    }
    
    public float GetCumulativeValue()
    {
        float cumulativeValue = 0;
        foreach (ItemModel item in items)
        {
            cumulativeValue += item.buyingPrice;
        }
        return cumulativeValue;
    }
}