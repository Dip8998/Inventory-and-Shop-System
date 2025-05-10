using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{
    public string itemID;
    public string itemName;
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
        itemName = so.itemName;
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
        itemName = so.itemName;
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
        Dictionary<ItemRarity, float> rarityThresholds = new Dictionary<ItemRarity, float>
        {
            { ItemRarity.VeryCommon, 0.0f },
            { ItemRarity.Common, 0.2f },
            { ItemRarity.Rare, 0.4f },
            { ItemRarity.Epic, 0.6f },
            { ItemRarity.Legendary, 0.8f }
        };

        float veryCommonThreshold = rarityThresholds[ItemRarity.VeryCommon] + rarityFactor * 0.1f;
        float commonThreshold = rarityThresholds[ItemRarity.Common] + rarityFactor * 0.1f;
        float rareThreshold = rarityThresholds[ItemRarity.Rare] + rarityFactor * 0.1f;
        float epicThreshold = rarityThresholds[ItemRarity.Epic] + rarityFactor * 0.1f;

        float randomValue = Random.Range(0f, 1f);

        if (randomValue >= epicThreshold)
        {
            rarity = ItemRarity.Legendary;
        }
        else if (randomValue >= rareThreshold)
        {
            rarity = ItemRarity.Epic;
        }
        else if (randomValue >= commonThreshold)
        {
            rarity = ItemRarity.Rare;
        }
        else if (randomValue >= veryCommonThreshold)
        {
            rarity = ItemRarity.Common;
        }
        else
        {
            rarity = ItemRarity.VeryCommon;
        }
    }

    public ItemSO GetItem() => itemSO;
}
