using UnityEngine;

public class ItemController
{
    private ItemModel model;
    private ItemView view;
    private UIService uiService;

    public ItemController(ItemModel model, ItemView view, UIService uiService)
    {
        this.model = model;
        this.view = view;
        this.uiService = uiService;
        view.UpdateView(model);
    }

    public void ShowItemDetails()
    {
        uiService.ShowItemDetails(model);
    }

    public ItemModel GetModel() => model;
}