using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUIDisplay : MonoBehaviour
{
    [SerializeField]
    private GameStatesManager gameStates;

    [SerializeField]
    private GameObject buttonInstance;

    [SerializeField]
    private Transform sitesTransform;

    public static Action activateSitesPanel;

    private void SpawnSites(string name)
    {
        //activate cities panel
        activateSitesPanel?.Invoke();

        Button[] buttons = sitesTransform.GetComponentsInChildren<Button>();

        List<string> sites = GameDataManager.instance.GetCity(name).interestPlaces;
        List<string> museums = GameDataManager.instance.GetCity(name).museums;

        while (buttons.Length < sites.Count || buttons.Length < museums.Count)
        {
            GameObject g = Instantiate(buttonInstance, sitesTransform);
        }

        if (buttons.Length >= sites.Count && buttons.Length >= museums.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < sites.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = sites[i];
                    buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(sites[i]); sitesTransform.gameObject.SetActive(false);});
                }
                if (i < museums.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = museums[i];
                    buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(museums[i]); sitesTransform.gameObject.SetActive(false);});

                }
                else if(i >= sites.Count && i >= museums.Count)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        GameStatesManager.showMoveUI += SpawnSites;
    }

    private void OnDisable()
    {
        GameStatesManager.showMoveUI -= SpawnSites;
    }
}
