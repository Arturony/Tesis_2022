using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    [SerializeField]
    private GameObject hudPanel;

    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private GameObject placePanel;

    [SerializeField]
    private GameObject loadingPanel;

    private void ActivateHudPanel()
    {
        if (hudPanel.activeInHierarchy)
            hudPanel.SetActive(false);
        else
            hudPanel.SetActive(true);
    }

    private void ActivateDialoguePanel()
    {
        if (dialoguePanel.activeInHierarchy)
            dialoguePanel.SetActive(false);
        else
            dialoguePanel.SetActive(true);
    }

    private void ActivatePlacePanel()
    {
        if (placePanel.activeInHierarchy)
            placePanel.SetActive(false);
        else
            placePanel.SetActive(true);
    }

    private void ActivateLoadingPanel()
    {
        if (loadingPanel.activeInHierarchy)
            loadingPanel.SetActive(false);
        else
            loadingPanel.SetActive(true);
    }

    private void OnEnable()
    {
        GameStatesManager.activateHUD += ActivateHudPanel;
        NpcUIDisplay.activateDialogue += ActivateDialoguePanel;
        GameStatesManager.activatePlaceBackground += ActivatePlacePanel;
        GameStatesManager.activateLoadingPanel += ActivateLoadingPanel;
    }

    private void OnDisable()
    {
        GameStatesManager.activateHUD -= ActivateHudPanel;
        NpcUIDisplay.activateDialogue -= ActivateDialoguePanel;
        GameStatesManager.activatePlaceBackground -= ActivatePlacePanel;
        GameStatesManager.activateLoadingPanel -= ActivateLoadingPanel;
    }
}
