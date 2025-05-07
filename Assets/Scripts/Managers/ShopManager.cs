using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopView shopView;
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private UIService uiService;
    [SerializeField] private ItemView itemViewPrefab;

    private void Start()
    {
        ShopModel shopModel = new ShopModel(itemList);

        shopView.InjectDependencies(itemViewPrefab, uiService, itemList);

        ShopController shopController = new ShopController(shopModel, shopView);
    }
}