using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    private ItemListSO allItemsList;
    private CurrencyManager currencyManager;
    private UIService uiService;
    private ShopModel shopModel;

    public InventoryController(InventoryModel model, InventoryView view, ItemListSO allItems, CurrencyManager currency, UIService uiService, ShopModel shopModel = null)
    {
        inventoryModel = model;
        inventoryView = view;
        allItemsList = allItems;
        currencyManager = currency;
        this.uiService = uiService;
        this.shopModel = shopModel;

        inventoryModel.SetInventoryController(this);
        inventoryView.SetController(this);

        EventService.Instance.OnGatherResourceButtonClickedEvent.AddListener(GatherResources);
        EventService.Instance.OnItemButtonClickedEvent.AddListener(ShowItemDetails);
        EventService.Instance.OnConfirmSellButtonClickedEvent.AddListener(ConfirmSellItem);
    }

    ~InventoryController()
    {
        EventService.Instance.OnGatherResourceButtonClickedEvent.RemoveListener(GatherResources);
        EventService.Instance.OnItemButtonClickedEvent.RemoveListener(ShowItemDetails);
        EventService.Instance.OnConfirmSellButtonClickedEvent.RemoveListener(ConfirmSellItem);
    }

    public void GatherResources()
    {
        AddItemForGathering();
    }

    private void AddItemForGathering()
    {
        if (inventoryModel.GetTotalWeight() >= inventoryModel.GetMaxWeight())
        {
            EventService.Instance.OnOverweightPopupEvent.InvokeEvent();
            return;
        }

        float cumulativeValue = inventoryModel.GetCumulativeValue();
        float rarityFactor = Mathf.Clamp01(cumulativeValue / 1000);

        int maxItemGenerate = Random.Range(1, 6);
        for (int i = 0; i < maxItemGenerate; i++)
        {
            int randomIndex = Random.Range(0, allItemsList.items.Count);
            ItemSO randomItemSO = allItemsList.items[randomIndex];
            int gatherQuantity = Random.Range(1, 5);

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
                    inventoryModel.AddItemInternal(gatheredItem); 
                }

                IncreaseTotalWeight(itemWeight);
                EventService.Instance.OnInventoryChangedEvent.InvokeEvent(); 
            }
            else
            {
                EventService.Instance.OnOverweightPopupEvent.InvokeEvent();
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
            inventoryModel.AddItemInternal(newItem); 
        }

        IncreaseTotalWeight(baseItem.weight * quantity);
        EventService.Instance.OnInventoryChangedEvent.InvokeEvent(); 
    }

    public void RemoveItem(ItemModel item, int quantity, int weightToRemove)
    {
        ItemModel existing = inventoryModel.GetItems().Find(i => i == item); 
        if (existing != null)
        {
            existing.quantity -= quantity;
            if (existing.quantity <= 0)
            {
                inventoryModel.RemoveItemInternal(existing); 
            }
            else
            {
                EventService.Instance.OnInventoryChangedEvent.InvokeEvent(); 
            }
            IncreaseTotalWeight(-weightToRemove);
        }
    }

    public List<ItemModel> GetItems()
    {
        return inventoryModel.GetItems();
    }

    private void ShowItemDetails(ItemModel itemModel)
    {
        uiService.ShowItemDetails(itemModel);
        EventService.Instance.OnItemButtonClickedEvent.InvokeEvent(itemModel);
    }

    private void ConfirmSellItem(ItemModel itemToSell)
    {
        if (itemToSell == null) return;

        int totalPrice = itemToSell.sellingPrice * uiService.selectedQuantity;
        int totalWeightToRemove = itemToSell.weight * uiService.selectedQuantity;

        currencyManager.AddCurrency(totalPrice);
        RemoveItem(itemToSell, uiService.selectedQuantity, totalWeightToRemove);
        EventService.Instance.OnInventoryChangedEvent.InvokeEvent(); 

        EventService.Instance.OnFeedbackTextRequestedEvent.InvokeEvent($"You gained {totalPrice} gold!");

        if (SoundService.Instance != null)
        {
            SoundService.Instance.Play(Sounds.COINCOLLECT);
        }
        else
        {
            Debug.LogWarning("SoundService Instance not found!");
        }
    }
}