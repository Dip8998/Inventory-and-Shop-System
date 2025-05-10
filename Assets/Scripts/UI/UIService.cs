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
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemBuyingPriceText;
    [SerializeField] private TextMeshProUGUI itemSellingPriceText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemWeightText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI confirmButtonText;
    [SerializeField] private TextMeshProUGUI feedbackText;

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
        EventService.Instance.OnButtonClickedEvent.AddListener(PlayButtonClickSoundInternal);
    }

    ~UIService()
    {
        if (EventService.Instance != null)
        {
            EventService.Instance.OnOverweightPopupEvent.RemoveListener(ShowOverweightPopupInternal);
            EventService.Instance.OnNotEnoughCurrencyPopupEvent.RemoveListener(ShowNotEnoughMoneyPopupInternal);
            EventService.Instance.OnFeedbackTextRequestedEvent.RemoveListener(ShowFeedbackTextInternal);
            EventService.Instance.OnPlusButtonClickedEvent.RemoveListener(OnIncreaseQuantity);
            EventService.Instance.OnMinusButtonClickedEvent.RemoveListener(OnDecreaseQuantity);
            EventService.Instance.OnButtonClickedEvent.RemoveListener(PlayButtonClickSoundInternal);
        }
    }

    private void ShowOverweightPopupInternal()
    {
        if (overweightPopupPanel != null)
        {
            overweightPopupPanel.SetActive(true);
            PlaySound(Sounds.ERROR);
        }
        else
        {
            Debug.LogWarning(UIConstants.OverweightPopupNotAssigned);
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
            PlaySound(Sounds.ERROR);
        }
        else
        {
            Debug.LogWarning(UIConstants.NotEnoughMoneyPopupNotAssigned);
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

        string itemName = selectedItemModel.itemName;
        int totalPrice = isSelling ? selectedItemModel.sellingPrice * selectedQuantity
                                   : selectedItemModel.buyingPrice * selectedQuantity;

        confirmationItemNameText.text = string.Format(isSelling ? UIConstants.ConfirmSell : UIConstants.ConfirmBuy);
        confirmationQuantityText.text = $"{selectedQuantity} x {itemName}";
        confirmationTotalText.text = string.Format(UIConstants.Total, totalPrice);

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
        if (inventoryView != null && inventoryManager != null && inventoryManager.inventoryController != null)
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
        itemNameText.text = string.Format(UIConstants.NameFormat, itemModel.itemName);
        itemTypeText.text = string.Format(UIConstants.TypeFormat, itemModel.itemType);
        itemDescriptionText.text = string.Format(UIConstants.DescriptionFormat, itemModel.itemDescription);
        itemBuyingPriceText.text = string.Format(UIConstants.BuyingPriceFormat, itemModel.buyingPrice);
        itemSellingPriceText.text = string.Format(UIConstants.SellingPriceFormat, itemModel.sellingPrice);
        itemRarityText.text = string.Format(UIConstants.RarityFormat, itemModel.rarity);
        itemWeightText.text = string.Format(UIConstants.WeightFormat, itemModel.weight);
        itemQuantityText.text = string.Format(UIConstants.QuantityFormat, itemModel.quantity);

        isSelling = !selectedItemModel.isFromShop;

        confirmButtonText.text = isSelling ? UIConstants.SellButtonText : UIConstants.BuyButtonText;
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(OnConfirmTransaction);

        itemDetailsPanel.SetActive(true);
    }

    public void UpdateWeightText()
    {
        if (inventoryManager != null && inventoryManager.inventoryController != null)
        {
            float currentWeight = inventoryManager.inventoryController.GetTotalWeight();
            float maxWeight = inventoryManager.inventoryController.GetMaxInventoryWeight();
            weightText.text = string.Format(UIConstants.WeightTextFormat, currentWeight, maxWeight);
        }
        else
        {
            Debug.LogWarning(UIConstants.InventoryManagerNotSetWeight);
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
        yield return new WaitForSeconds(UIConstants.FeedbackTextDisplayDuration);
        feedbackText.gameObject.SetActive(false);
    }

    private void PlaySound(Sounds sound)
    {
        if (SoundService.Instance != null)
        {
            SoundService.Instance.Play(sound);
        }
        else
        {
            Debug.LogWarning(UIConstants.SoundServiceNotFound);
        }
    }

    private void PlayErrorSound()
    {
        PlaySound(Sounds.ERROR);
    }

    private void PlayButtonClickSoundInternal()
    {
        PlaySound(Sounds.BUTTONCLICK);
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