using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemID;
    public ItemType itemType;
    public GameObject itemPrefab;
    public Sprite itemSprite;
    public Sprite itemRarityBG;
    public string itemDescription;
    public int itemBuyingPrice;
    public int itemSellingPrice;
    public int itemWeight;
    public ItemRarity itemRarity;
    public int itemQuantity;
}
