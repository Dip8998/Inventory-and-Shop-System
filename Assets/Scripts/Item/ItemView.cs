using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
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

    private Button thisButton;
    private ItemController itemController;

    public void UpdateView(ItemModel model)
    {
        itemImage.sprite = model.itemSprite;
        itemRarityBG.sprite = model.itemRarityBG;
        itemName.text = model.itemType.ToString();
        itemDescription.text = model.itemDescription;
        itemBuyingPrice.text = model.buyingPrice.ToString();
        itemSellingPrice.text = model.sellingPrice.ToString();
        itemRarity.text = model.rarity.ToString();
        itemWeight.text = model.weight.ToString();
        itemQuantity.text = model.quantity.ToString();
    }

    public void SetController(ItemController controller)
    {
        itemController = controller;

        if (thisButton == null)
            thisButton = GetComponent<Button>();

        thisButton.onClick.RemoveAllListeners(); 
        thisButton.onClick.AddListener(itemController.ShowItemDetails);
    }
}
