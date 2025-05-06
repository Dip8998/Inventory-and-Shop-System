using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private GameObject itemContainerPrefab;
    

    public void GetAllItems()
    {
        clearAllItems();
        for (int i = 0; i < itemList.items.Count; i++)
        {
            itemContainerPrefab.transform.GetChild(0).GetComponent<Image>().sprite = itemList.items[i].itemRarityBG;
            itemContainerPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemList.items[i].itemQuantity.ToString();
            itemContainerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = itemList.items[i].itemSprite;
            GameObject item = Instantiate(itemContainerPrefab, itemContainer);
        }
    }

    public void GetWeaponItems()
    {
        clearAllItems();

        for (int i = 0; i < itemList.items.Count; i++)
        {
            if(itemList.items[i].itemType == ItemType.Weapons)
            {
                itemContainerPrefab.transform.GetChild(0).GetComponent<Image>().sprite = itemList.items[i].itemRarityBG;
                itemContainerPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemList.items[i].itemQuantity.ToString();
                itemContainerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = itemList.items[i].itemSprite;
                GameObject item = Instantiate(itemContainerPrefab, itemContainer);
            }
        }
    }

    public void GetConsumableItems()
    {
        clearAllItems();

        for (int i = 0; i < itemList.items.Count; i++)
        {
            if (itemList.items[i].itemType == ItemType.Consumables)
            {
                itemContainerPrefab.transform.GetChild(0).GetComponent<Image>().sprite = itemList.items[i].itemRarityBG;
                itemContainerPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemList.items[i].itemQuantity.ToString();
                itemContainerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = itemList.items[i].itemSprite;
                GameObject item = Instantiate(itemContainerPrefab, itemContainer);
            }
        }
    }

    public void GetMaterialItems()
    {
        clearAllItems();

        for (int i = 0; i < itemList.items.Count; i++)
        {
            if (itemList.items[i].itemType == ItemType.Materials)
            {
                itemContainerPrefab.transform.GetChild(0).GetComponent<Image>().sprite = itemList.items[i].itemRarityBG;
                itemContainerPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemList.items[i].itemQuantity.ToString();
                itemContainerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = itemList.items[i].itemSprite;
                GameObject item = Instantiate(itemContainerPrefab, itemContainer);
            }
        }
    }

    public void GetTreasureItems()
    {
        clearAllItems();

        for (int i = 0; i < itemList.items.Count; i++)
        {
            if (itemList.items[i].itemType == ItemType.Treasures)
            {
                itemContainerPrefab.transform.GetChild(0).GetComponent<Image>().sprite = itemList.items[i].itemRarityBG;
                itemContainerPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemList.items[i].itemQuantity.ToString();
                itemContainerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = itemList.items[i].itemSprite;
                GameObject item = Instantiate(itemContainerPrefab, itemContainer);
            }
        }
    }

    public void clearAllItems()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
