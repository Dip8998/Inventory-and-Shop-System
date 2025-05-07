using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemListScriptableObject", menuName = "ScriptableObjects/ItemListSO")]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> items;
}