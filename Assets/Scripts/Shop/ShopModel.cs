using System.Collections.Generic;

public class ShopModel
{
    private ShopController shopController;

    private ItemListSO itemListSO;

    public ShopModel(ItemListSO inventorySO)
    {
        itemListSO = inventorySO;
    }

    public List<ItemSO> GetAllItems()
    {
        return itemListSO.items;
    }

    public void SetShopController(ShopController controller)
    {
        shopController = controller;
    }
}
