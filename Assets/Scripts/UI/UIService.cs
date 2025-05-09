using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject itemDetailsPanel;
    [SerializeField] private ShopView shopView;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private InventoryManager inventoryManager;
    [HideInInspector] public ItemModel selectedItemModel;
    [HideInInspector] public int selectedQuantity = 1;
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

    void Awake()
    {
        EventService.Instance.OnOverweightPopupEvent.AddListener(ShowOverweightPopupInternal);
        EventService.Instance.OnNotEnoughCurrencyPopupEvent.AddListener(ShowNotEnoughMoneyPopupInternal);
        EventService.Instance.OnFeedbackTextRequestedEvent.AddListener(ShowFeedbackTextInternal);
        EventService.Instance.OnPlusButtonClickedEvent.AddListener(OnIncreaseQuantity);
        EventService.Instance.OnMinusButtonClickedEvent.AddListener(OnDecreaseQuantity);
    }

    ~UIService()
    {
        EventService.Instance.OnOverweightPopupEvent.RemoveListener(ShowOverweightPopupInternal);
        EventService.Instance.OnNotEnoughCurrencyPopupEvent.RemoveListener(ShowNotEnoughMoneyPopupInternal);
        EventService.Instance.OnFeedbackTextRequestedEvent.RemoveListener(ShowFeedbackTextInternal);
        EventService.Instance.OnPlusButtonClickedEvent.RemoveListener(OnIncreaseQuantity);
        EventService.Instance.OnMinusButtonClickedEvent.RemoveListener(OnDecreaseQuantity);
    }

    private void ShowOverweightPopupInternal()
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

    private void ShowNotEnoughMoneyPopupInternal()
    {
        if (popUpForNotEnoughMoney != null)
        {
            popUpForNotEnoughMoney.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Not Enough Money Popup not assigned in UIService!");
        }
    }

    public void CloseNotEnoughMoneyPopup()
    {
        if (popUpForNotEnoughMoney != null)
        {
            popUpForNotEnoughMoney.SetActive(false);
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

        if (isSelling)
        {
            EventService.Instance.OnConfirmSellButtonClickedEvent?.InvokeEvent(selectedItemModel);
        }
        else
        {
            EventService.Instance.OnConfirmBuyButtonClickedEvent?.InvokeEvent(selectedItemModel.GetItem());
        }

        confirmationPopupPanel.SetActive(false);
        itemDetailsPanel.SetActive(false);
        UpdateWeightText();
        if (inventoryView != null)
        {
            inventoryView.UpdateInventoryUI(inventoryManager.inventoryController.GetItems());
        }
        if (shopView != null && shopManager != null && shopManager.shopControllerInstance != null)
        {
            shopManager.shopControllerInstance.ShowAllItems(shopView.GetCurrentFilter());
        }
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
        confirmButton.onClick.AddListener(OnConfirmTransaction); // This button click triggers the confirmation popup

        itemDetailsPanel.SetActive(true);
    }

    public void UpdateWeightText()
    {
        if (inventoryManager != null && inventoryManager.inventoryController != null)
        {
            float currentWeight = inventoryManager.inventoryController.GetTotalWeight();
            float maxWeight = inventoryManager.inventoryController.GetMaxInventoryWeight();
            weightText.text = $"Weight: {currentWeight}/{maxWeight} kg";
        }
        else
        {
            Debug.LogWarning("Inventory Manager or Controller not set in UIService for weight update!");
        }
    }

    public void OnIncreaseQuantity()
    {
        if (selectedItemModel == null) return;
        if (!isSelling && selectedQuantity < GetShopItemQuantity(selectedItemModel.itemID))
            selectedQuantity++;
        else if (isSelling && selectedQuantity < selectedItemModel.quantity)
            selectedQuantity++;

        UpdateQuantityText();
    }

    private int GetShopItemQuantity(string itemID)
    {
        if (shopManager != null && shopManager.shopControllerInstance != null)
        {
            ItemSO shopItem = shopManager.shopControllerInstance.GetShopModel().GetShopItemByID(itemID);
            return shopItem != null ? shopItem.itemQuantity : 0;
        }
        return 0;
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

    private void ShowFeedbackTextInternal(string message)
    {
        StartCoroutine(ShowFeedbackTextCoroutine(message));
    }

    private IEnumerator ShowFeedbackTextCoroutine(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        feedbackText.gameObject.SetActive(false);
    }

    private void PlayTransactionSoundInternal(bool isBuying)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(isBuying ? buySound : sellSound);
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned in UIService!");
        }
    }

    private void UpdateQuantityText()
    {
        quantityText.text = selectedQuantity.ToString();
    }

    public void SetInventoryManager(InventoryManager manager)
    {
        inventoryManager = manager;
    }

    public void SetShopManager(ShopManager manager)
    {
        shopManager = manager;
    }
}