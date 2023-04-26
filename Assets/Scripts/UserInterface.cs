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
    [SerializeField] private GameObject m_ForagingUI;

    Image[] m_InventoryImages = new Image[8];

    [SerializeField] private Sprite m_DefaultEmptyIcon;

    //Foraging minigame
    [SerializeField] private Text m_ClicksText;
    private int m_Clicks;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("MinigameFishing", ShowFishingUI);
        EventManager.StartListening("MinigameForaging", ShowForagingUI);
        EventManager.StartListening("OnMinigameEnd", HideFishingUI);
        EventManager.StartListening("OnMinigameEnd", HideForagingUI);
        EventManager.StartListening("StartInteracting", ShowInteractionUI);
        EventManager.StartListening("StopInteracting", HideInteractionUI);
        EventManager.StartListening("Pause", ShowPauseMenu);
        EventManager.StartListening("Unpause", HidePauseMenu);
        EventManager.StartListening("Reset", ResetInventoryUI);
        EventManager.StartListening("OnForageClick", UpdateClicks);

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
    private void ShowForagingUI()
    {
        m_ForagingUI.SetActive(true);
    }

    private void HideForagingUI()
    {
        m_Clicks = 0;
        m_ClicksText.text = "0";
        m_ForagingUI.SetActive(false);
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
        Button b = m_PauseMenu.transform.Find("End Game Button").GetComponent<Button>();

        if (m_Inventory.CheckIfInventoryIsFilled())
        {  
            b.interactable = true;
        }
        else
        {
            b.interactable = false;
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
  
    private void ResetInventoryUI()
    {
        for (int i = 0; i < m_InventoryImages.Length; i++)
        {
            UpdateInventory(m_DefaultEmptyIcon, i);
        }
    }

    private void UpdateClicks()
    {
        m_Clicks += 1;
        m_ClicksText.text = m_Clicks.ToString();
    }

    public void LoadGameButton()
    {
        EventManager.TriggerEvent("OnLoadGame");
    }

    public void EndGameButton()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundEnum.EndGame);
        EventManager.TriggerEvent("EndGame");
        m_PauseMenu.SetActive(false);
    }


    private void OnDestroy()
    {
        EventManager.StopListening("MinigameFishing", ShowFishingUI);
        EventManager.StopListening("MinigameForaging", ShowForagingUI);
        EventManager.StopListening("OnMinigameEnd", HideFishingUI);
        EventManager.StopListening("OnMinigameEnd", HideForagingUI);
        EventManager.StopListening("StartInteracting", ShowInteractionUI);
        EventManager.StopListening("StopInteracting", HideInteractionUI);
        EventManager.StopListening("Pause", ShowPauseMenu);
        EventManager.StopListening("Unpause", HidePauseMenu);
        EventManager.StopListening("Reset", ResetInventoryUI);
        EventManager.StopListening("OnForageClick", UpdateClicks);
    }
}
