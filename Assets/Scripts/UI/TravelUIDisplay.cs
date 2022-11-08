using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TravelUIDisplay : MonoBehaviour
{
    [SerializeField]
    private GameStatesManager gameStates;

    [SerializeField]
    private GameObject buttonInstance;

    [SerializeField]
    private Transform countryParent;

    [SerializeField]
    private Transform cityParent;

    [SerializeField]
    private GameObject cityContainer;

    public static Action activateTravelPanel;

    private void ShowContries()
    {
        activateTravelPanel?.Invoke();
        countryParent.gameObject.SetActive(true);
        cityContainer.gameObject.SetActive(false);


        if (countryParent.gameObject.GetComponentsInChildren<Button>().Length != GameDataManager.instance.GetCountryNames().Count)
        {
            //create the buttons
            foreach(string s in GameDataManager.instance.GetCountryNames())
            {
                GameObject g = Instantiate(buttonInstance, countryParent);
                g.GetComponentInChildren<TMP_Text>().text = s;

                g.GetComponent<Button>().onClick.AddListener(delegate { SpawnCities(s); });
            }
        }

    }

    private void SpawnCities(string name)
    {
        //activate cities panel
        countryParent.gameObject.SetActive(false);
        cityContainer.gameObject.SetActive(true);
        

        Button[] buttons = cityParent.GetComponentsInChildren<Button>();

        List<string> cities = GameDataManager.instance.GetCountry(name).cities;

        while(buttons.Length < cities.Count)
        {
            GameObject g = Instantiate(buttonInstance, cityParent);
            buttons = cityParent.GetComponentsInChildren<Button>();
        }

        if(buttons.Length >= cities.Count)
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                if(i < cities.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = cities[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    AddListener(b, cities[i]);
                }
                else
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
        GameStatesManager.showTravelUI += ShowContries;
    }

    private void OnDisable()
    {
        GameStatesManager.showTravelUI -= ShowContries;
    }
}
