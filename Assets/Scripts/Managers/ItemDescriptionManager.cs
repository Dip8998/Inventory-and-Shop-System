using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemBuyingPriceText;
    [SerializeField] private TextMeshProUGUI itemSellingPriceText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemWeightText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private Button increaseItemQuantityButton;
    [SerializeField] private Button decreaseItemQuantityButton;
    [SerializeField] private Button buyItemButton;

    public void SetItemDescription(
        string itemType,
        string itemDescription,
        int itemBuyingPrice,
        int itemSellingPrice,
        string itemRarity,
        int itemWeight,
        int itemQuantity
        )
    {
        itemTypeText.text = $"Type: {itemType}";
        itemDescriptionText.text = $"Item Description: {itemDescription}";
        itemBuyingPriceText.text = $"Buying Price: {itemBuyingPrice.ToString()}";
        itemSellingPriceText.text = $"Selling Price: {itemSellingPrice.ToString()}";
        itemRarityText.text = $"Rarity: {itemRarity}";
        itemWeightText.text = $"Weight: {itemWeight.ToString()}";
        itemQuantityText.text = $"Quantity: {itemQuantity.ToString()}";
    }


}
