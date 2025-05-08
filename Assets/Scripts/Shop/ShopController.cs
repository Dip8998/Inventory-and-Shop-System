using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private InventoryController inventoryController;
    private CurrencyManager currencyController; 

    public ShopController(ShopModel model, ShopView view, InventoryController inventoryController, CurrencyManager currencyController) 
    { 
        shopModel = model;
        shopView = view;
        this.inventoryController = inventoryController;
        this.currencyController = currencyController; 

        shopModel.SetShopController(this);
        shopView.SetController(this);
    }

    public void ShowAllItems(ItemType? filterType = null)
    {
        List<ItemSO> items = shopModel.GetAllShopItems();
        ShuffleList(items);
        ClearAllItems();

        foreach (ItemSO itemSO in items)
        {
            if (filterType != null && itemSO.itemType != filterType) continue;

            ItemModel itemModel = new ItemModel(itemSO);
            ItemView viewInstance = GameObject.Instantiate(shopView.GetItemViewPrefab(), shopView.GetItemContainer());
            ItemController itemController = new ItemController(itemModel, viewInstance, shopView.GetUIService());
            viewInstance.SetController(itemController);
        }
    }

    public void BuyItem(ItemSO itemToBuy)
    {
        if (currencyController.GetCurrentCurrency() >= itemToBuy.itemBuyingPrice)
        {
            currencyController.RemoveCurrency(itemToBuy.itemBuyingPrice);
            shopModel.RemoveShopItem(itemToBuy.itemID); 
            ShowAllItems(shopView.GetCurrentFilter());
        }
    }

    private void ClearAllItems()
    {
        foreach (Transform child in shopView.GetItemContainer())
            GameObject.Destroy(child.gameObject);
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }
}
