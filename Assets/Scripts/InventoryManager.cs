using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager m_Instance;
    void Start()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }

        EventManager.StartListening("OnLoadGame", LoadGame);
        EventManager.StartListening("Reset", ResetInventory);

    }

    public static InventoryManager GetInstance()
    {
        return m_Instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private Items[] m_InventorySlots = new Items[8];
    public Items[] inventoryslots => m_InventorySlots;
    [SerializeField] private Item m_ItemSO;
    private Action<Sprite, int> OnInventoryAdd;
    private bool m_IsLoading = false;

    [System.Serializable]
    public enum Items
    {
        Nothing,
        Carp,
        Pike,
        Piranha,
        KingFish,
        Wood
    }

    private void Update()
    {
        //This is for testing, feel free to use it if you get bored
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddInventoryItem(Items.Carp);
        }
    }

    public void AssignListener(Action<Sprite, int> method)
    {
        OnInventoryAdd += method;
    }

    public void AddInventoryItem(Items item)
    {
        int indexref = -1;

        for (int i = 0; i < m_InventorySlots.Length; i++)
        {
            if (m_InventorySlots[i] == Items.Nothing)
            {
                m_InventorySlots[i] = item;
                indexref = i;

                if (!m_IsLoading)
                    SaveSystem.SaveData(this);
                break;
            }
        }

        for (int y = 0; y < m_ItemSO.ItemData.Count; y++)
        {
            if (m_ItemSO.ItemData[y].item == item)
            {
                if (indexref > -1)
                {
                    OnInventoryAdd?.Invoke(m_ItemSO.ItemData[y].icon, indexref);
                    break;
                }
                else
                {
                    Debug.Log("Inventory is full");
                }
            }
        }
    }

    public bool CheckIfInventoryIsFilled()
    {
        for (int i = 0; i < m_InventorySlots.Length; i++)
        {
            if (m_InventorySlots[i] == Items.Nothing)
            {
                return false;
            }
        }
        return true;
    }

    private void LoadGame()
    {
        m_IsLoading = true;
        GameData data = SaveSystem.LoadData();

        ResetInventory();

        for (int i = 0; i < data.m_InventorySlots.Length; i++)
        {
            AddInventoryItem(data.m_InventorySlots[i]);
        }

        Debug.Log("Loaded");
        m_IsLoading = false;
        EventManager.TriggerEvent("Unpause");
    }

    private void ResetInventory()
    {
        m_InventorySlots = new Items[8];
    }
}
