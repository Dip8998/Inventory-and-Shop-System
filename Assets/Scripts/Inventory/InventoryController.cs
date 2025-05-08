using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    private ItemListSO allItemsList;
    private CurrencyManager currencyManager;


    public InventoryController(InventoryModel model, InventoryView view, ItemListSO allItems, CurrencyManager currency)
    {
        inventoryModel = model;
        inventoryView = view;
        allItemsList = allItems;
        currencyManager = currency;

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
        if (inventoryModel.GetTotalWeight() >= inventoryModel.GetMaxWeight())
        {
            if (inventoryView.GetUIService() != null)
            {
                inventoryView.GetUIService().ShowOverweightPopup();
            }
            else
            {
                Debug.LogError("UIService reference not set in InventoryController!");
            }
            return;
        }

        float cumulativeValue = inventoryModel.GetCumulativeValue();
        float rarityFactor = Mathf.Clamp01(cumulativeValue / 1000);

        int maxItemGenerate = Random.Range(1, 6);
        for (int i = 0; i < maxItemGenerate; i++)
        {
            int randomIndex = Random.Range(0, allItemsList.items.Count);
            ItemSO randomItemSO = allItemsList.items[randomIndex];
            int gatherQuantity = Random.Range(1, 4);

            int itemWeight = randomItemSO.itemWeight * gatherQuantity;

            if (inventoryModel.GetTotalWeight() + itemWeight <= inventoryModel.GetMaxWeight())
            {
                ItemModel existingItem = inventoryModel.GetItems().Find(item => item.itemID == randomItemSO.itemID);

                if (existingItem != null)
                {
                    existingItem.quantity += gatherQuantity;
                }
                else
                {
                    ItemModel gatheredItem = new ItemModel(randomItemSO, false); 
                    gatheredItem.quantity = gatherQuantity;
                    gatheredItem.AdjustRarity(rarityFactor);
                    inventoryModel.GetItems().Add(gatheredItem);
                }

                IncreaseTotalWeight(itemWeight);
                UpdateInventoryView();
            }
            else
            {
                inventoryView.GetUIService().ShowOverweightPopup(); 
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

    public bool CanAfford(int cost)
    {
        return currencyManager.GetCurrentCurrency() >= cost;
    }

    public void RemoveCurrency(int amount)
    {
        currencyManager.RemoveCurrency(amount);
    }

    public void AddCurrency(int amount)
    {
        currencyManager.AddCurrency(amount);
    }

    public bool CanAddWeight(int weightToAdd)
    {
        return GetTotalWeight() + weightToAdd <= GetMaxInventoryWeight();
    }

    public void AddItem(ItemModel baseItem, int quantity)
    {
        ItemModel existing = inventoryModel.GetItems().Find(i => i.itemType == baseItem.itemType && i.rarity == baseItem.rarity);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            ItemModel newItem = new ItemModel(baseItem.GetItem(), false);
            newItem.quantity = quantity;
            inventoryModel.GetItems().Add(newItem);
        }

        IncreaseTotalWeight(baseItem.weight * quantity);
        UpdateInventoryView();
    }

    public void RemoveItem(ItemModel item, int quantity, int weightToRemove)
    {
        item.quantity -= quantity;
        if (item.quantity <= 0)
        {
            inventoryModel.GetItems().Remove(item);
        }

        IncreaseTotalWeight(-weightToRemove);
        UpdateInventoryView();
    }

    public List<ItemModel> GetItems()
    {
        return inventoryModel.GetItems();
    }
}