using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ItemSO itemScriptableObject;
    [SerializeField] private ItemDescriptionManager itemDescriptionManager;

    void Start()
    {
        
    }

    public void ItemDescription()
    {
        itemDescriptionManager.SetItemDescription(
            CheckItemType(),
            this.itemScriptableObject.itemDescription,
            this.itemScriptableObject.itemBuyingPrice,
            this.itemScriptableObject.itemSellingPrice,
            CheckItemRarity(),
            this.itemScriptableObject.itemWeight,
            this.itemScriptableObject.itemQuantity
            );
    }

    public string CheckItemType()
    {
        switch(itemScriptableObject.itemType)
        {
            case ItemType.Weapons:
                return "Weapon";

            case ItemType.Consumables:
                return "Consumable";

            case ItemType.Materials:
                return "Material";

            case ItemType.Treasures:
                return "Treasure";
        }
        return null;
    }

    public string CheckItemRarity()
    {
        switch(itemScriptableObject.itemRarity)
        {
            case ItemRarity.VeryCommon:
                return "VeryCommon";

            case ItemRarity.Common:
                return "Common";

            case ItemRarity.Rare:
                return "Rare";
            
            case ItemRarity.Epic:
                return "Epic";
            
            case ItemRarity.Legendary:
                return "Legendary";
        }
        return null;
    }
}
