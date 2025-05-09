public class EventService
{
    private static EventService instance;

    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    // Inventory Related Events
    public EventController OnGatherResourceButtonClickedEvent { get; private set; }
    public EventController<ItemModel> OnItemButtonClickedEvent { get; private set; }
    public EventController<ItemModel> OnConfirmSellButtonClickedEvent { get; private set; }
    public EventController OnOverweightPopupEvent { get; private set; }

    // Shop Related Events
    public EventController<ItemType?> OnItemTypeButtonClickedEvent { get; private set; }
    public EventController<ItemSO> OnBuyButtonClickedEvent { get; private set; }
    public EventController<ItemSO> OnConfirmBuyButtonClickedEvent { get; private set; }

    // UI Related Events
    public EventController OnPlusButtonClickedEvent { get; private set; }
    public EventController OnMinusButtonClickedEvent { get; private set; }
    public EventController<string> OnFeedbackTextRequestedEvent { get; private set; }
    public EventController OnNotEnoughCurrencyPopupEvent { get; private set; }

    public EventService()
    {
        // Initialize Inventory Events
        OnGatherResourceButtonClickedEvent = new EventController();
        OnItemButtonClickedEvent = new EventController<ItemModel>();
        OnConfirmSellButtonClickedEvent = new EventController<ItemModel>();
        OnOverweightPopupEvent = new EventController();

        // Initialize Shop Events
        OnItemTypeButtonClickedEvent = new EventController<ItemType?>();
        OnBuyButtonClickedEvent = new EventController<ItemSO>();
        OnConfirmBuyButtonClickedEvent = new EventController<ItemSO>();

        // Initialize UI Events
        OnPlusButtonClickedEvent = new EventController();
        OnMinusButtonClickedEvent = new EventController();
        OnFeedbackTextRequestedEvent = new EventController<string>();
        OnNotEnoughCurrencyPopupEvent = new EventController();
    }
}