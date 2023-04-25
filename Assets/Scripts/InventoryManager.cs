using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static InventoryManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    Items[] m_InventorySlots = new Items[8];
    private Action<Sprite> OnInventoryAdd;

    [System.Serializable]
    public enum Items
    {
        Carp,
        Pike,
        Piranha,
        KingFish
    }

    [System.Serializable]
    private struct ItemStruct
    {
        public Items item;
        public string name;
        public Sprite icon;
    }

    [SerializeField] private List<ItemStruct> ItemData;

    public void AssignListener(Action<Sprite> method)
    {
        OnInventoryAdd += method;
    }

    public void AddInventoryItem(Items item)
    {
        for (int i = 0; i < ItemData.Count; i++)
        {
            if (ItemData[i].item == item)
            {
                OnInventoryAdd?.Invoke(ItemData[i].icon);
            }
        }
    }
}
