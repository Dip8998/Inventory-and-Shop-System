using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{
    public string itemID;
    public ItemType itemType;
    public Sprite itemSprite;
    public Sprite itemRarityBG; 
    public string itemDescription; 
    public int buyingPrice; 
    public int sellingPrice; 
    public int weight; 
    public ItemRarity rarity;
    public int quantity;
    private ItemSO itemSO;
    public bool isFromShop;

    public ItemModel(ItemSO so, bool fromShop = true) 
    {
        itemID = so.itemID;
        itemType = so.itemType;
        itemSprite = so.itemSprite;
        itemRarityBG = so.itemRarityBG;
        itemDescription = so.itemDescription;
        buyingPrice = so.itemBuyingPrice;
        sellingPrice = so.itemSellingPrice;
        weight = so.itemWeight;
        rarity = so.itemRarity;
        quantity = so.itemQuantity;
        itemSO = so;
        isFromShop = fromShop; 
    }

    public ItemModel(ItemSO so)
    {
        itemID = so.itemID;
        itemType = so.itemType;
        itemSprite = so.itemSprite;
        itemRarityBG = so.itemRarityBG;
        itemDescription = so.itemDescription;
        buyingPrice = so.itemBuyingPrice;
        sellingPrice = so.itemSellingPrice;
        weight = so.itemWeight;
        rarity = so.itemRarity;
        quantity = so.itemQuantity;
        itemSO = so;
        isFromShop = false;
    }

    public void AdjustRarity(float rarityFactor)
    {
        Dictionary<ItemRarity, int> baseRarityValues = new Dictionary<ItemRarity, int>
        {
            { ItemRarity.VeryCommon, 10 },
            { ItemRarity.Common, 20 },
            { ItemRarity.Rare, 30 },
            { ItemRarity.Epic, 40 },
            { ItemRarity.Legendary, 50 }
        };

        Dictionary<ItemRarity, int> adjustedRarityValues = new Dictionary<ItemRarity, int>();

        foreach (var kvp in baseRarityValues)
        {
            adjustedRarityValues[kvp.Key] = Mathf.Max(0, kvp.Value - (int)(kvp.Value * rarityFactor));
        }

        string debugString = "Rarity Factor: " + rarityFactor;
        foreach (var kvp in adjustedRarityValues)
        {
            debugString += ", Adjusted " + kvp.Key + ": " + kvp.Value;
        }
        Debug.Log(debugString);

        if (adjustedRarityValues[ItemRarity.Legendary] <= 0)
            rarity = ItemRarity.Legendary;
        else if (adjustedRarityValues[ItemRarity.Epic] <= 0)
            rarity = ItemRarity.Epic;
        else if (adjustedRarityValues[ItemRarity.Rare] <= 0)
            rarity = ItemRarity.Rare;
        else if (adjustedRarityValues[ItemRarity.Common] <= 0)
            rarity = ItemRarity.Common;
        else
            rarity = ItemRarity.Common;

        if (rarity == ItemRarity.Common)
        {
            Debug.LogWarning("Rarity is Common after adjustment. This may not be intended.");
        }
    }

    public ItemSO GetItem() => itemSO;
}
