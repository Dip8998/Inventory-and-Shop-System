using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public ItemListSO itemListScriptablObject;
    public Transform inventoryContainer;

    private void Start()
    {
        initialize();
    }

    public void initialize()
    {
        List<ItemSO> randomItems = new List<ItemSO>(itemListScriptablObject.items);
        ShuffleList(randomItems);

        foreach (ItemSO itemData in randomItems)
        {
            if (itemData.itemPrefab == null)
            {
                Debug.LogWarning("Item prefab is null.");
                continue;
            }

            GameObject itemPrefab = itemData.itemPrefab;

            itemPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemData.itemQuantity.ToString();
            itemPrefab.transform.GetChild(2).GetComponent<Image>().sprite = itemData.itemSprite;
            itemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = itemData.itemRarityBG;

            Instantiate(itemPrefab, inventoryContainer);
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
