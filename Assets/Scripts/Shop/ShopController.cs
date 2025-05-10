using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private InventoryController inventoryController;
    private CurrencyManager currencyManager;
    private UIService uiService;

    public ShopController(ShopModel model, ShopView view, InventoryController inventoryController, CurrencyManager currencyManager, UIService uiService)
    {
        shopModel = model;
        shopView = view;
        this.inventoryController = inventoryController;
        this.currencyManager = currencyManager;
        this.uiService = uiService;

        shopModel.SetShopController(this);
        shopView.SetController(this);

        EventService.Instance.OnItemTypeButtonClickedEvent.AddListener(ShowAllItems);
        EventService.Instance.OnBuyButtonClickedEvent.AddListener(AttemptBuyItem);
        EventService.Instance.OnConfirmBuyButtonClickedEvent.AddListener(ConfirmBuyItem);
    }

    ~ShopController()
    {
        EventService.Instance.OnItemTypeButtonClickedEvent.RemoveListener(ShowAllItems);
        EventService.Instance.OnBuyButtonClickedEvent.RemoveListener(AttemptBuyItem);
        EventService.Instance.OnConfirmBuyButtonClickedEvent.RemoveListener(ConfirmBuyItem);
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

    private void AttemptBuyItem(ItemSO itemToBuySO)
    {
        if (itemToBuySO == null) return;
        uiService.ShowItemDetails(new ItemModel(itemToBuySO, true)); 
    }

    private void ConfirmBuyItem(ItemSO itemToBuySO)
    {
        if (itemToBuySO == null) return;

        int quantity = uiService.selectedQuantity; 
        int totalPrice = itemToBuySO.itemBuyingPrice * quantity;
        int totalWeight = itemToBuySO.itemWeight * quantity;

        if (!currencyManager.CanAfford(totalPrice))
        {
            EventService.Instance.OnNotEnoughCurrencyPopupEvent.InvokeEvent(); 
            return;
        }

        if (!inventoryController.CanAddWeight(totalWeight))
        {
            EventService.Instance.OnOverweightPopupEvent.InvokeEvent(); 
            return;
        }

        currencyManager.RemoveCurrency(totalPrice);
        inventoryController.AddItem(new ItemModel(itemToBuySO, false), quantity);
        shopModel.DecreaseShopItemQuantity(itemToBuySO.itemID, quantity);

        EventService.Instance.OnFeedbackTextRequestedEvent.InvokeEvent($"You bought {itemToBuySO.itemName} x{quantity}!");
        SoundService.Instance.Play(Sounds.BUYSOUND);
        ShowAllItems(shopView.GetCurrentFilter()); 
    }
}