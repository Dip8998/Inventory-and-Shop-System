using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private InventoryController inventoryController;
    private CurrencyManager currencyManager;

    public ShopController(ShopModel model, ShopView view, InventoryController inventoryController, CurrencyManager currencyManager)
    {
        shopModel = model;
        shopView = view;
        this.inventoryController = inventoryController;
        this.currencyManager = currencyManager;

        shopModel.SetShopController(this);
        shopView.SetController(this);
    }

    public void ShowAllItems(ItemType? filterType = null)
    {
        List<ItemSO> allShopItems = shopModel.GetAllShopItems(); 
        ClearAllItems();

        foreach (ItemSO itemSO in allShopItems)
        {
            if (filterType != null && itemSO.itemType != filterType) continue;

            ItemModel itemModel = new ItemModel(itemSO, true);

            ItemView viewInstance = GameObject.Instantiate(shopView.GetItemViewPrefab(), shopView.GetItemContainer());
            ItemController itemController = new ItemController(itemModel, viewInstance, shopView.GetUIService());
            viewInstance.SetController(itemController);

            ItemSO currentShopItemData = shopModel.GetShopItemByID(itemSO.itemID);
            if (currentShopItemData != null)
            {
                ItemModel updatedItemModel = new ItemModel(currentShopItemData, true);
                viewInstance.UpdateView(updatedItemModel); 
            }
        }
    }
    private void ClearAllItems()
    {
        foreach (Transform child in shopView.GetItemContainer())
            GameObject.Destroy(child.gameObject);
    }

    public ShopModel GetShopModel()
    {
        return shopModel;
    }
}
