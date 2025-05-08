using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopView shopView;
    [SerializeField] private InventoryView inventoryView; 
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private UIService uiService;
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private CurrencyManager currencyManager; 

    private void Start()
    {
        InventoryModel inventoryModel = new InventoryModel();

        InventoryController inventoryController = new InventoryController(inventoryModel, inventoryView, itemList);

        ShopModel shopModel = new ShopModel(itemList);

        shopView.InjectDependencies(itemViewPrefab, uiService, itemList);

        ShopController shopController = new ShopController(shopModel, shopView, inventoryController, currencyManager);
    }
}
