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
            ItemSO copiedItem = ScriptableObject.CreateInstance<ItemSO>();
            copiedItem.itemID = itemSO.itemID;
            copiedItem.itemType = itemSO.itemType;
            copiedItem.itemSprite = itemSO.itemSprite;
            copiedItem.itemRarityBG = itemSO.itemRarityBG;
            copiedItem.itemDescription = itemSO.itemDescription;
            copiedItem.itemBuyingPrice = itemSO.itemBuyingPrice;
            copiedItem.itemSellingPrice = itemSO.itemSellingPrice;
            copiedItem.itemWeight = itemSO.itemWeight;
            copiedItem.itemRarity = itemSO.itemRarity;
            copiedItem.itemQuantity = itemSO.itemQuantity; 
            copiedItem.itemPrefab = itemSO.itemPrefab; 

            displayedItemInShop.Add(copiedItem);
        }
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
            ItemSO copiedItem = ScriptableObject.CreateInstance<ItemSO>();
            copiedItem.itemID = itemToAdd.itemID;
            copiedItem.itemType = itemToAdd.itemType;
            copiedItem.itemSprite = itemToAdd.itemSprite;
            copiedItem.itemRarityBG = itemToAdd.itemRarityBG;
            copiedItem.itemDescription = itemToAdd.itemDescription;
            copiedItem.itemBuyingPrice = itemToAdd.itemBuyingPrice;
            copiedItem.itemSellingPrice = itemToAdd.itemSellingPrice;
            copiedItem.itemWeight = itemToAdd.itemWeight;
            copiedItem.itemRarity = itemToAdd.itemRarity;
            copiedItem.itemQuantity = itemToAdd.itemQuantity; 
            copiedItem.itemPrefab = itemToAdd.itemPrefab; 

            displayedItemInShop.Add(copiedItem);
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