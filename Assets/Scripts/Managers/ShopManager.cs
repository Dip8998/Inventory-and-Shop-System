using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopView shopView;
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private UIService uiService;
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private InventoryManager inventoryManager; 

    [HideInInspector] public ShopController shopControllerInstance;
    [HideInInspector]public ShopModel shopModelInstance;

    private void Start()
    {
        InventoryModel inventoryModel = inventoryManager.inventoryModel; 
        InventoryController inventoryController = inventoryManager.inventoryController; 

        shopModelInstance = new ShopModel(itemList);
        shopView.InjectDependencies(itemViewPrefab, uiService, itemList);
        shopControllerInstance = new ShopController(shopModelInstance, shopView, inventoryController, currencyManager, uiService); 

        uiService.SetShopManager(this); 
    }
}