using System.Collections.Generic;

public class ShopModel
{
    private ShopController shopController;

    private ItemListSO itemListSO;

    private List<ItemSO> displayedItemInShop = new List<ItemSO>();

    public ShopModel(ItemListSO inventorySO)
    {
        itemListSO = inventorySO;
        displayedItemInShop = new List<ItemSO>(itemListSO.items);
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
            displayedItemInShop.Add(itemToAdd);
        }
    }
}
