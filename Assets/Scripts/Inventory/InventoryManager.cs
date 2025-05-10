using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ItemListSO allGatherableItems;
    [SerializeField] private UIService uiService;
    [SerializeField] private ItemView itemView;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private ShopManager shopManager;

    [HideInInspector] public InventoryModel inventoryModel;
    [HideInInspector] public InventoryController inventoryController;

    void Awake()
    {
        inventoryModel = new InventoryModel();
        inventoryController = new InventoryController(inventoryModel, inventoryView, allGatherableItems, currencyManager, uiService, shopManager?.shopModelInstance);
        inventoryView.InjectDependencies(itemView, uiService);

        uiService.SetInventoryManager(this);

        inventoryView.UpdateInventoryUI(inventoryModel.GetItems());
    }

    public void OnGatherResourcesButtonClicked()
    {
        SoundService.Instance.Play(Sounds.GATHERRESOURCE);
        EventService.Instance.OnGatherResourceButtonClickedEvent.InvokeEvent();
    }
}