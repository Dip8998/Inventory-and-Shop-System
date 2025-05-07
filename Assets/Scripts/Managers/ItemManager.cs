using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public ItemListSO itemListScriptablObject;
    [SerializeField] private ItemService itemService;
    [SerializeField] private Transform itemContainer;


    public void DisplayItems(ItemType? itemType = null)
    {
        clearAllItems();

        List<ItemSO> randomItems = new List<ItemSO>(itemListScriptablObject.items);
        ShuffleList(randomItems);

        foreach (ItemSO item in itemListScriptablObject.items)
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

    public void clearAllItems()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
