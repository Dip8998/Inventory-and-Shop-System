using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryController inventoryController;

    [SerializeField] private Transform inventoryItemContainer;
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private UIService uiService;

    public void SetController(InventoryController controller)
    {
        inventoryController = controller;
    }

    public void InjectDependencies(ItemView prefab, UIService ui)
    {
        itemViewPrefab = prefab;
        uiService = ui;
    }
}