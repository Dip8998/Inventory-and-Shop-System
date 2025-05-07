using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemService : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image itemRarityBG;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemBuyingPrice;
    [SerializeField] private TextMeshProUGUI itemSellingPrice;
    [SerializeField] private TextMeshProUGUI itemRarity;
    [SerializeField] private TextMeshProUGUI itemWeight;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    [SerializeField] private ItemDetails itemDetails;

    [SerializeField] private Canvas itemCanvas;

    private void Start()
    {
        itemCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void Initialize(
        Sprite itemImage, 
        Sprite itemRarityBG,
        string itemName, 
        string itemDescription,
        int itemBuyingPrice,
        int itemSellingPrice,
        string itemRarity,
        int itemWeight,
        int itemQuantity
        )
    {
        this.itemImage.sprite = itemImage;
        this.itemRarityBG.sprite = itemRarityBG;
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
        this.itemBuyingPrice.text = itemBuyingPrice.ToString();
        this.itemSellingPrice.text = itemSellingPrice.ToString();
        this.itemRarity.text = itemRarity;
        this.itemWeight.text = itemWeight.ToString();
        this.itemQuantity.text = itemQuantity.ToString();
    }

    public void SetDetails()
    {
        itemDetails.Initialize(
        itemImage.sprite,
        itemRarityBG.sprite,
        itemName.text,
        itemDescription.text,
        int.Parse(itemBuyingPrice.text),
        int.Parse(itemSellingPrice.text),
        itemRarity.text,
        int.Parse(itemWeight.text),
        int.Parse(itemQuantity.text)
        );
        ItemDetails details = Instantiate(itemDetails, itemCanvas.transform);
    }

    public Sprite  GetItemImage()
    {
        return itemImage.sprite;
    }

    public TextMeshProUGUI GetItemName()
    {
        return itemName;
    }

    public TextMeshProUGUI GetItemDescription()
    {
        return itemDescription;
    }

    public TextMeshProUGUI GetItemBuyingPrice()
    {
        return itemBuyingPrice;
    }

    public TextMeshProUGUI GetItemSellingPrice()
    {
        return itemSellingPrice;
    }

    public TextMeshProUGUI GetItemRarity()
    {
        return itemRarity;
    }

    public TextMeshProUGUI GetItemWeight()
    {
        return itemWeight;
    }

    public TextMeshProUGUI GetItemQuantity()
    {
        return itemQuantity;
    }
}
