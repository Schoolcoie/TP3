using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [System.Serializable]
    public struct ItemStruct
    {
        public InventoryManager.Items item;
        public string name;
        public Sprite icon;
    }

    public List<ItemStruct> ItemData = new List<ItemStruct>();
}
