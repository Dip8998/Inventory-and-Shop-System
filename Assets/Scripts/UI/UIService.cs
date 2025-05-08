using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject itemDetailsPanel;
    [SerializeField] private ShopView shopView;
    [SerializeField] private ShopManager shopManager;
    private ItemModel selectedItemModel;
    private InventoryController inventoryController;
    private ItemSO items;
    private int selectedQuantity = 1;
    private bool isSelling = false;

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
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button confirmButton; 
    [SerializeField] private TextMeshProUGUI confirmButtonText;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip sellSound;

    [Header("Confirmation Popup")]
    public GameObject confirmationPopupPanel;
    public TextMeshProUGUI confirmationItemNameText;
    public TextMeshProUGUI confirmationQuantityText;
    public TextMeshProUGUI confirmationTotalText;
    public GameObject overweightPopupPanel;
    public GameObject popUpForNotEnoughMoney;

    public void ShowOverweightPopup()
    {
        if (overweightPopupPanel != null)
        {
            overweightPopupPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Overweight Popup Panel not assigned in UIService!");
        }
    }

    public void CloseOverweightPopup()
    {
        if (overweightPopupPanel != null)
        {
            overweightPopupPanel.SetActive(false);
        }
    }

    public void ShowConfirmationPopup()
    {
        if (selectedItemModel == null) return;

        string itemName = selectedItemModel.itemType.ToString();
        int totalPrice = isSelling ? selectedItemModel.sellingPrice * selectedQuantity
                                   : selectedItemModel.buyingPrice * selectedQuantity;

        confirmationItemNameText.text = $"Confirm {(isSelling ? "Sell" : "Buy")}";
        confirmationQuantityText.text = $"{selectedQuantity} x {itemName}";
        confirmationTotalText.text = $"Total: {totalPrice} Gold";

        confirmationPopupPanel.SetActive(true);
    }

    public void OnConfirmYesClicked()
    {
        if (selectedItemModel == null) return;

        int totalPrice = isSelling ? selectedItemModel.sellingPrice * selectedQuantity
                                   : selectedItemModel.buyingPrice * selectedQuantity;
        int totalWeight = selectedItemModel.weight * selectedQuantity;

        if (isSelling)
        {
            inventoryController.AddCurrency(totalPrice);
            inventoryController.RemoveItem(selectedItemModel, selectedQuantity, totalWeight);
            audioSource.PlayOneShot(sellSound);
            StartCoroutine(ShowFeedbackText($"You gained {totalPrice} gold!"));
        }
        else // Buying
        {
            if (!inventoryController.CanAfford(totalPrice))
            {
                popUpForNotEnoughMoney.SetActive(true);
                confirmationPopupPanel.SetActive(false);
                return;
            }

            if (!inventoryController.CanAddWeight(totalWeight))
            {
                overweightPopupPanel.SetActive(true);
                confirmationPopupPanel.SetActive(false);
                return;
            }

            inventoryController.RemoveCurrency(totalPrice);
            inventoryController.AddItem(selectedItemModel, selectedQuantity);

            if (shopManager != null)
            {
                ShopController shopController = shopManager.shopControllerInstance; 
                if (shopController != null)
                {
                    ShopModel shopModel = shopController.GetShopModel();
                    if (shopModel != null)
                    {
                        shopModel.DecreaseShopItemQuantity(selectedItemModel.itemID, selectedQuantity);
                    }
                    shopController.ShowAllItems(shopView.GetCurrentFilter()); 
                }
                else
                {
                    Debug.LogError("ShopController instance is null in ShopManager!");
                }
            }
            else
            {
                Debug.LogError("ShopManager reference not set in UIService!");
            }

            audioSource.PlayOneShot(buySound);
            StartCoroutine(ShowFeedbackText($"You bought {selectedItemModel.itemType} x{selectedQuantity}!"));
        }

        confirmationPopupPanel.SetActive(false);
        itemDetailsPanel.SetActive(false);
        inventoryController.UpdateInventoryView();
    }

    public void OnConfirmNoClicked()
    {
        confirmationPopupPanel.SetActive(false);
    }

    public void ShowItemDetails(ItemModel itemModel)
    {
        selectedItemModel = itemModel;
        selectedQuantity = 1;
        UpdateQuantityText();

        itemImage.sprite = itemModel.itemSprite;
        itemRarityBG.sprite = itemModel.itemRarityBG;
        itemName.text = $"Type - {itemModel.itemType}";
        itemDescription.text = $"Item Description - {itemModel.itemDescription}";
        itemBuyingPrice.text = $"Buying Price - {itemModel.buyingPrice}";
        itemSellingPrice.text = $"Selling Price - {itemModel.sellingPrice}";
        itemRarity.text = $"Rarity - {itemModel.rarity}";
        itemWeight.text = $"Weight - {itemModel.weight}";
        itemQuantity.text = $"Quantity - {itemModel.quantity}";

        isSelling = !selectedItemModel.isFromShop; 

        confirmButtonText.text = isSelling ? "Sell" : "Buy";
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(OnConfirmTransaction);

        itemDetailsPanel.SetActive(true);
    }

    public void UpdateWeightText()
    {
        float currentWeight = inventoryController.GetTotalWeight();
        float maxWeight = inventoryController.GetMaxInventoryWeight();
        weightText.text = $"Weight: {currentWeight}/{maxWeight} kg";
    }

    public void OnIncreaseQuantity()
    {
        if (selectedItemModel == null) return;
        if (!isSelling && selectedQuantity < selectedItemModel.quantity) 
            selectedQuantity++;
        else if (isSelling && selectedQuantity < selectedItemModel.quantity)
            selectedQuantity++;

        UpdateQuantityText();
    }

    public void OnDecreaseQuantity()
    {
        if (selectedQuantity > 1)
            selectedQuantity--;

        UpdateQuantityText();
    }

    public void OnConfirmTransaction()
    {
        ShowConfirmationPopup();
    }

    private IEnumerator ShowFeedbackText(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        feedbackText.gameObject.SetActive(false);
    }

    private void UpdateQuantityText()
    {
        quantityText.text = selectedQuantity.ToString();
    }


    public void SetInventoryController(InventoryController controller)
    {
        inventoryController = controller;
    }
}
