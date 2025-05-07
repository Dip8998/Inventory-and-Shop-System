using UnityEngine;

public class ItemModel
{
    public Sprite itemSprite;
    public Sprite itemRarityBG;
    public ItemType itemType;
    public string itemDescription;
    public int buyingPrice;
    public int sellingPrice;
    public ItemRarity rarity;
    public int weight;
    public int quantity;
    public ItemSO itemSO;

    public ItemModel(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        itemSprite = itemSO.itemSprite;
        itemRarityBG = itemSO.itemRarityBG;
        itemType = itemSO.itemType;
        itemDescription = itemSO.itemDescription;
        buyingPrice = itemSO.itemBuyingPrice;
        sellingPrice = itemSO.itemSellingPrice;
        rarity = itemSO.itemRarity;
        weight = itemSO.itemWeight;
        quantity = itemSO.itemQuantity;
    }
}
