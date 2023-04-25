using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private InventoryManager m_Inventory;
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private GameObject m_InteractUI;
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_FishingUI;

    Image[] m_InventoryImages = new Image[8];

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("MinigameFishing", ShowFishingUI);
        EventManager.StartListening("OnMinigameEnd", HideFishingUI);
        EventManager.StartListening("StartInteracting", ShowInteractionUI);
        EventManager.StartListening("StopInteracting", HideInteractionUI);
        EventManager.StartListening("Pause", ShowPauseMenu);
        EventManager.StartListening("Unpause", HidePauseMenu);

        m_Inventory.AssignListener(UpdateInventory);

        int i = 0;

        foreach (Transform t in transform.Find("InventoryUI"))
        { 
            if (t.name.Contains("Inventory"))
            {
                m_InventoryImages[i] = t.GetChild(0).GetComponent<Image>();
                i++;
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
        if (m_Inventory.CheckIfInventoryIsFilled())
        {
            Button b = m_PauseMenu.transform.Find("End Game Button").GetComponent<Button>();
            b.interactable = true;
        }
        m_PauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        m_PauseMenu.SetActive(false);
    }

    public void UpdateInventory(Sprite icon, int index)
    {
        m_InventoryImages[index].sprite = icon;
    }
    public void LoadGameButton()
    {
        EventManager.TriggerEvent("OnLoadGame");
    }

    public void EndGameButton()
    {
        Debug.Log("Game Ended");
        EventManager.TriggerEvent("EndGame");
    }
}
