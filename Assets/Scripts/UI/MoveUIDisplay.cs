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

        while (buttons.Length < sites.Count + museums.Count)
        {
            GameObject g = Instantiate(buttonInstance, sitesTransform);
            buttons = sitesTransform.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= sites.Count + museums.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(i < sites.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = sites[i];
                    Button b = buttons[i];
                    AddListener(b, sites[i]);
                }
                else if (i >= sites.Count && i - sites.Count < museums.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = museums[i - sites.Count];
                    Button b = buttons[i];
                    AddListener(b, museums[i - sites.Count]);

                }
                else if(i >= sites.Count + museums.Count)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void AddListener(Button b, string value)
    {
        b.onClick.AddListener(() => gameStates.SelectPlace(value));
        //b.onClick.AddListener(() => sitesTransform.gameObject.SetActive(false));
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
