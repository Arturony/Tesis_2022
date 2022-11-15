using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfoDisplay : MonoBehaviour
{

    [SerializeField]
    private GameStatesManager gameStates;

    [SerializeField]
    private GameObject buttonInstance;

    [SerializeField]
    private GameObject objectParent;

    [SerializeField]
    private Transform objectContainer;

    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private Transform buttonsContainer;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text caracText;

    [SerializeField]
    private TMP_Text descText;

    public static Action activateObjectPanel;
    public void ShowContries()
    {
        objectParent.SetActive(true);

        Button[] buttons = objectContainer.GetComponentsInChildren<Button>();

        List<string> countries = GameDataManager.instance.GetCountryNames();

        while (buttons.Length < countries.Count)
        {
            GameObject g = Instantiate(buttonInstance, objectContainer);
            buttons = objectContainer.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= countries.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < countries.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = countries[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    b.GetComponentInChildren<TMP_Text>().fontSize = 24;
                    AddListenerCountries(b, countries[i]);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

    }

    private void SpawnCities(string name)
    {
        //activate cities panel
        //objectParent.SetActive(true);


        Button[] buttons = buttonsContainer.GetComponentsInChildren<Button>();

        List<string> cities = GameDataManager.instance.GetCountry(name).cities;

        while (buttons.Length < cities.Count)
        {
            GameObject g = Instantiate(buttonInstance, buttonsContainer);
            buttons = buttonsContainer.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= cities.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < cities.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = cities[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    AddListenerCities(b, cities[i]);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void SpawnSites(string name)
    {
        //activate cities panel
        //objectParent.SetActive(true);

        Button[] buttons = buttonsContainer.GetComponentsInChildren<Button>();

        List<string> sites = GameDataManager.instance.GetCity(name).interestPlaces;
        List<string> museums = GameDataManager.instance.GetCity(name).museums;

        while (buttons.Length < sites.Count + museums.Count)
        {
            GameObject g = Instantiate(buttonInstance, buttonsContainer);
            buttons = buttonsContainer.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= sites.Count + museums.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < sites.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = sites[i];
                    Button b = buttons[i];
                    AddListenerPlace(b, sites[i]);
                }
                else if (i >= sites.Count && i - sites.Count < museums.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = museums[i - sites.Count];
                    Button b = buttons[i];
                    AddListenerMuseum(b, museums[i - sites.Count]);

                }
                else if (i >= sites.Count + museums.Count)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void SpawnArtPieces(string name)
    {
        //activate cities panel
        //objectParent.SetActive(true);


        Button[] buttons = buttonsContainer.GetComponentsInChildren<Button>();

        List<string> cities = GameDataManager.instance.GetMuseum(name).piecesId;

        while (buttons.Length < cities.Count)
        {
            GameObject g = Instantiate(buttonInstance, buttonsContainer);
            buttons = buttonsContainer.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= cities.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < cities.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = cities[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    AddListenerArtPieces(b, cities[i]);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ShowRobbers()
    {
        objectParent.SetActive(true);

        Button[] buttons = objectContainer.GetComponentsInChildren<Button>();

        List<string> countries = GameDataManager.instance.GetAllRobbers();

        while (buttons.Length < countries.Count)
        {
            GameObject g = Instantiate(buttonInstance, objectContainer);
            buttons = objectContainer.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= countries.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < countries.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = countries[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    b.GetComponentInChildren<TMP_Text>().fontSize = 24;
                    AddListenerRobbers(b, countries[i]);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

    }

    public void SpawnArtPiecesMenu()
    {
        //activate cities panel
        //objectParent.SetActive(true);
        objectParent.SetActive(true);

        Button[] buttons = objectContainer.GetComponentsInChildren<Button>();

        List<string> cities = GameDataManager.instance.GetArtPiecesNames();

        while (buttons.Length < cities.Count)
        {
            GameObject g = Instantiate(buttonInstance, objectContainer);
            buttons = objectContainer.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= cities.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < cities.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = cities[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    b.GetComponentInChildren<TMP_Text>().fontSize = 24;
                    AddListenerArtPieces(b, cities[i]);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void SetCountryInfo(string name)
    {
        Country c = GameDataManager.instance.GetCountry(name);

        nameText.text = name;
        caracText.text = "";
        descText.text = c.description;
        buttonsContainer.gameObject.SetActive(true);
        SpawnCities(name);
    }

    private void SetCityInfo(string name)
    {
        City c = GameDataManager.instance.GetCity(name);

        nameText.text = name;
        caracText.text = "Pais: " + c.country;
        descText.text = c.description;
        buttonsContainer.gameObject.SetActive(true);
        SpawnSites(name);
    }

    private void SetPlaceInfo(string name)
    {
        InterestSite c = GameDataManager.instance.GetSite(name);

        nameText.text = c.name;
        caracText.text = "Pais: " + c.country + " \nCiudad: " + c.city + " \nDireccion: " + c.location;
        descText.text = c.description;
        buttonsContainer.gameObject.SetActive(false);
        //SpawnCities(name);
    }

    private void SetMuseumInfo(string name)
    {
        Museum c = GameDataManager.instance.GetMuseum(name);

        nameText.text = c.name;
        caracText.text = "Pais: " + c.country + " \nCiudad: " + c.city + " \nDireccion: " + c.location;
        descText.text = c.description;
        buttonsContainer.gameObject.SetActive(true);
        SpawnArtPieces(name);
    }

    private void SetArtPiecesInfo(string name)
    {
        ArtPiece c = GameDataManager.instance.GetArtPiece(name);

        nameText.text = c.title;
        caracText.text = "Creador: " + c.creator + " \nFecha: " + c.date + " \nOrigen: " + c.origin + " \nMuseo: " + c.museum;
        descText.text = c.description;
        buttonsContainer.gameObject.SetActive(false);
    }

    private void SetRobbersInfo(string name)
    {
        Robber c = GameDataManager.instance.GetRobber(name);

        nameText.text = c.robberName;
        caracText.text = "Caracteristicas: \n";
        for(int i = 0; i < c.tags.Count-1; i++)
        {
            caracText.text += "- " + c.tags[i] + "\n";
        }
        caracText.text += "- " + c.tags[c.tags.Count - 1];
        descText.text = c.description;
        buttonsContainer.gameObject.SetActive(false);
    }

    void AddListenerCountries(Button b, string value)
    {
        b.onClick.AddListener(() => SetCountryInfo(value));
        b.onClick.AddListener(() => infoPanel.SetActive(true));
    }

    void AddListenerCities(Button b, string value)
    {
        b.onClick.AddListener(() => SetCityInfo(value));
        b.onClick.AddListener(() => infoPanel.SetActive(true));
    }

    void AddListenerPlace(Button b, string value)
    {
        b.onClick.AddListener(() => SetPlaceInfo(value));
        b.onClick.AddListener(() => infoPanel.SetActive(true));
    }

    void AddListenerMuseum(Button b, string value)
    {
        b.onClick.AddListener(() => SetMuseumInfo(value));
        b.onClick.AddListener(() => infoPanel.SetActive(true));
    }

    void AddListenerArtPieces(Button b, string value)
    {
        b.onClick.AddListener(() => SetArtPiecesInfo(value));
        b.onClick.AddListener(() => infoPanel.SetActive(true));
    }

    void AddListenerRobbers(Button b, string value)
    {
        b.onClick.AddListener(() => SetRobbersInfo(value));
        b.onClick.AddListener(() => infoPanel.SetActive(true));
    }

}
