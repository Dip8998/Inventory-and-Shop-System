using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private Image itemRarityBG;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemBuyingPriceText;
    [SerializeField] private TextMeshProUGUI itemSellingPriceText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemWeightText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;

    public void Initialize(
        Sprite itemSprite,
        Sprite itemRarityBG,
        string itemType, 
        string itemDescription, 
        int buyingPrice, 
        int sellingPrice, 
        string itemRarity, 
        int itemWeight, 
        int itemQuantity
        ) 
    {
        this.itemSprite.sprite = itemSprite;
        this.itemRarityBG.sprite = itemRarityBG;
        itemTypeText.text = $"Type - {itemType}";
        itemDescriptionText.text = $"Item Description - {itemDescription}";
        itemBuyingPriceText.text = $"Buying Price - {buyingPrice.ToString()}";
        itemSellingPriceText.text = $"Selling Price - {sellingPrice.ToString()}";
        itemRarityText.text = $"Rarity - {itemRarity}";
        itemWeightText.text = $"Weight - {itemWeight.ToString()}";
        itemQuantityText.text = $"Quantity - {itemQuantity.ToString()}";
    }
}
