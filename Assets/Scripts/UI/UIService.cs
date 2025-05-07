using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private ItemService itemService;    

    public void GetAllItems() => DisplayItems();

    public void GetWeaponItems() => DisplayItems(ItemType.Weapons);

    public void GetConsumableItems() => DisplayItems(ItemType.Consumables);

    public void GetMaterialItems() => DisplayItems(ItemType.Materials);

    public void GetTreasureItems() => DisplayItems(ItemType.Treasures);

    public void clearAllItems()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void DisplayItems(ItemType? itemType = null)
    {
        clearAllItems();

        foreach(ItemSO item in itemList.items)
        {
            if (itemType != null && item.itemType != itemType.Value)
                continue;

            ItemService instance = Instantiate(itemService, itemContainer);
            instance.Initialize(
                item.itemSprite,
                item.itemRarityBG,
                item.itemType.ToString(),
                item.itemDescription,
                item.itemBuyingPrice,
                item.itemSellingPrice,
                item.itemRarity.ToString(),
                item.itemWeight,
                item.itemQuantity
                );
        }
    }
}
