using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemSprite;
    public Sprite itemRarityBG;
    public string itemDescription;
    public int itemBuyingPrice;
    public int itemSellingPrice;
    public int itemWeight;
    public ItemRarity itemRarity;
    public int itemQuantity;
}
