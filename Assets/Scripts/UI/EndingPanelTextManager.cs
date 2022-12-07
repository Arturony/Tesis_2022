using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingPanelTextManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text winText;

    [SerializeField]
    private TMP_Text loseText;

    private void SetWinText()
    {
        winText.text = "Has capturado a " + GameDataManager.instance.GetCurrentMission().GetRobber().robberName + " y " + GameDataManager.instance.GetCurrentMission().GetArtPieceRobbed().title + " ha sido recuperdada. \n Buen Trabajo!";
    }

    private void SetLoseByTimeText()
    {
        loseText.text = "¡Te has demorado mucho! El ladrón ha escapado y " + GameDataManager.instance.GetCurrentMission().GetArtPieceRobbed().title + " se ha perdido para siempre.";
    }

    private void SetLoseByRobberText(string robber)
    {
        loseText.text = "Has acusado falsamente a " + robber + " y mientras tanto, el ladrón verdadero ha escapado!";
    }

    private void OnEnable()
    {
        GameStatesManager.setLoseByRobberText += SetLoseByRobberText;
        GameStatesManager.setLoseByTimeText += SetLoseByTimeText;
        GameStatesManager.setWinText += SetWinText;
    }

    private void OnDisable()
    {
        GameStatesManager.setLoseByRobberText -= SetLoseByRobberText;
        GameStatesManager.setLoseByTimeText -= SetLoseByTimeText;
        GameStatesManager.setWinText -= SetWinText;
    }
}
