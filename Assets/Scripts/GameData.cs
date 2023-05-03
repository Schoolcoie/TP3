using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public InventoryManager.Items[] m_InventorySlots = new InventoryManager.Items[8];

    public GameData(InventoryManager inventoryManager)
    {
        m_InventorySlots = inventoryManager.inventoryslots;
    }
}
