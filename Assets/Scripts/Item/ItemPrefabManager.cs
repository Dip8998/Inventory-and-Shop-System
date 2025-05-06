using UnityEngine;
using UnityEngine.UIElements;

public class ItemPrefabManager : MonoBehaviour
{
    public Image image;
    public Image itemRarityImage;
    public ItemPrefabManager(Sprite _sprite, Image itemRarityImage)
    {
        image.sprite = _sprite;
        this.itemRarityImage = itemRarityImage;
    }
}
