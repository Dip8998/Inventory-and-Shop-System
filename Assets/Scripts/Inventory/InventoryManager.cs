using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ItemListSO allGatherableItems;
    [SerializeField] private UIService uiService;
    [SerializeField] private ItemView itemView;
    [SerializeField] private CurrencyManager currencyManager;

    private InventoryModel inventoryModel;
    private InventoryController inventoryController;

    void Start()
    {
        inventoryModel = new InventoryModel();
        inventoryController = new InventoryController(inventoryModel, inventoryView, allGatherableItems, currencyManager);
        inventoryView.InjectDependencies(itemView, uiService);

        uiService.SetInventoryController(inventoryController);

        inventoryView.UpdateInventoryUI(inventoryModel.GetItems());
    }

    public void OnGatherResourcesButtonClicked()
    {
        inventoryController.GatherResources();
    }
}