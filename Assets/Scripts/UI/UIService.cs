using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager;

    public void GetAllItems() => itemManager.DisplayItems();

    public void GetWeaponItems() => itemManager.DisplayItems(ItemType.Weapons);

    public void GetConsumableItems() => itemManager.DisplayItems(ItemType.Consumables);

    public void GetMaterialItems() => itemManager.DisplayItems(ItemType.Materials);

    public void GetTreasureItems() => itemManager.DisplayItems(ItemType.Treasures);
}
