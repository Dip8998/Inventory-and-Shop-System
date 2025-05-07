using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject itemDetailsPanel;
    private ItemModel itemModel;
    private InventoryController inventoryController;

    [Header("UI Elements")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Image itemRarityBG;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemBuyingPrice;
    [SerializeField] private TextMeshProUGUI itemSellingPrice;
    [SerializeField] private TextMeshProUGUI itemRarity;
    [SerializeField] private TextMeshProUGUI itemWeight;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private TextMeshProUGUI weightText;

    public void ShowItemDetails(ItemModel itemModel)
    {
        this.itemModel = itemModel;
        itemImage.sprite = itemModel.itemSprite;
        itemRarityBG.sprite = itemModel.itemRarityBG;
        itemName.text = $"Type - {itemModel.itemType}";
        itemDescription.text = $"Item Description - {itemModel.itemDescription}";
        itemBuyingPrice.text = $"Buying Price - {itemModel.buyingPrice.ToString()}";
        itemSellingPrice.text = $"Selling Price - {itemModel.sellingPrice.ToString()}";
        itemRarity.text = $"Rarity - {itemModel.rarity}";
        itemWeight.text = $"Weight - {itemModel.weight.ToString()}";
        itemQuantity.text = $"Quantity - {itemModel.quantity.ToString()}";

        itemDetailsPanel.SetActive(true);
    }

    public void UpdateWeightText()
    {
        float currentWeight = inventoryController.GetTotalWeight();
        float maxWeight = inventoryController.GetMaxInventoryWeight();
        weightText.text = $"Weight: {currentWeight}/{maxWeight} kg";
    }

    public void SetInventoryController(InventoryController controller)
    {
        inventoryController = controller;
    }
}
