using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    private ShopController shopController;
    public ItemListSO itemListSO;
    private List<ItemSO> displayedItemInShop = new List<ItemSO>();

    public ShopModel(ItemListSO inventorySO)
    {
        itemListSO = inventorySO;
        displayedItemInShop = new List<ItemSO>();
        foreach (ItemSO itemSO in itemListSO.items)
        {
            displayedItemInShop.Add(CreateShopItemCopy(itemSO));
        }
    }

    private ItemSO CreateShopItemCopy(ItemSO originalItem)
    {
        ItemSO copiedItem = ScriptableObject.CreateInstance<ItemSO>();
        copiedItem.itemID = originalItem.itemID;
        copiedItem.itemType = originalItem.itemType;
        copiedItem.itemSprite = originalItem.itemSprite;
        copiedItem.itemRarityBG = originalItem.itemRarityBG;
        copiedItem.itemDescription = originalItem.itemDescription;
        copiedItem.itemBuyingPrice = originalItem.itemBuyingPrice;
        copiedItem.itemSellingPrice = originalItem.itemSellingPrice;
        copiedItem.itemWeight = originalItem.itemWeight;
        copiedItem.itemRarity = originalItem.itemRarity;
        copiedItem.itemQuantity = originalItem.itemQuantity;
        copiedItem.itemPrefab = originalItem.itemPrefab;
        return copiedItem;
    }

    public void SetShopController(ShopController controller)
    {
        shopController = controller;
    }

    public List<ItemSO> GetAllShopItems() => displayedItemInShop;

    public ItemSO GetShopItemByID(string id)
    {
        return displayedItemInShop.Find(item => item.itemID == id);
    }

    public void RemoveShopItem(string id)
    {
        displayedItemInShop.RemoveAll(item => item.itemID == id);
    }

    public void AddShopItem(ItemSO itemToAdd)
    {
        if (!displayedItemInShop.Exists(i => i.itemID == itemToAdd.itemID))
        {
            displayedItemInShop.Add(CreateShopItemCopy(itemToAdd));
        }
    }

    public void DecreaseShopItemQuantity(string itemID, int quantityBought)
    {
        ItemSO shopItem = displayedItemInShop.Find(item => item.itemID == itemID);
        if (shopItem != null)
        {
            shopItem.itemQuantity -= quantityBought;
            if (shopItem.itemQuantity <= 0)
            {
                displayedItemInShop.Remove(shopItem);
            }
        }
    }
}