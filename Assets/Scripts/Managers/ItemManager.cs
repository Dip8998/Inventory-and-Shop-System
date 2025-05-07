using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemListSO itemListSO;
    [SerializeField] private ItemView itemView;
    [SerializeField] private UIService itemUIService;
    [SerializeField] private Transform itemContainer;

    public void DisplayItems(ItemType? filterType = null)
    {
        ClearItems();

        List<ItemSO> shuffledItems = new List<ItemSO>(itemListSO.items);
        ShuffleList(shuffledItems);

        foreach (ItemSO itemSO in shuffledItems)
        {
            if (filterType != null && itemSO.itemType != filterType) continue;

            ItemModel model = new ItemModel(itemSO);
            ItemView instance = GameObject.Instantiate(itemView,itemContainer);
            ItemController controller = new ItemController(model,instance,itemUIService);
            instance.SetController(controller);
        }
    }

    private void ClearItems()
    {
        foreach (Transform child in itemContainer)
            Destroy(child.gameObject);
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }
}
