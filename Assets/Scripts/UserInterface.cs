using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private GameObject m_InteractUI;
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_FishingUI;

    Sprite[] m_Inventory = new Sprite[8];
    List<Image> m_InventorySlots = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("MinigameFishing", ShowFishingUI);
        EventManager.StartListening("StopFishing", HideFishingUI);
        EventManager.StartListening("StartInteracting", ShowInteractionUI);
        EventManager.StartListening("StopInteracting", HideInteractionUI);
        EventManager.StartListening("Pause", ShowPauseMenu);
        EventManager.StartListening("Unpause", HidePauseMenu);

        foreach (Transform t in transform.Find("InventoryUI"))
        {
            if (t.name.Contains("Inventory"))
            {
                m_InventorySlots.Add(t.GetChild(0).GetComponent<Image>());
            }
        } 
    }


    private void ShowFishingUI()
    {
        m_FishingUI.SetActive(true);
    }

    private void HideFishingUI()
    {
        m_FishingUI.SetActive(false);
    }

    private void ShowInteractionUI()
    {
        m_InteractUI.SetActive(true);
    }

    private void HideInteractionUI()
    {
        m_InteractUI.SetActive(false);
    }

    private void ShowPauseMenu()
    {
        if (CheckIfInventoryIsFilled())
        {
            Button b = m_PauseMenu.transform.Find("End Game Button").GetComponent<Button>();
            b.interactable = true;
        }
        m_PauseMenu.SetActive(true);
    }

    private bool CheckIfInventoryIsFilled()
    {
        for (int i = 0; i < m_Inventory.Length; i++)
        {
            if (m_Inventory[i] == null)
            {
                print("Inventory slot empty");
                return false;
            }
            print("Inventory slot filled");
        }
        return true;
    }

    private void HidePauseMenu()
    {
        m_PauseMenu.SetActive(false);
    }

    public void EndGame()
    {
        Debug.Log("Game Ended");
        EventManager.TriggerEvent("EndGame");
    }

    public void UpdateInventory(Sprite icon)
    {
        for (int i = 0; i < m_Inventory.Length; i++)
        {
            if (m_Inventory[i] == null)
            {
                m_Inventory.SetValue(icon, i);

                m_InventorySlots[i].sprite = icon;

                break;
            }
        }
    }
}
