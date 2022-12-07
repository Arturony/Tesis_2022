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

    [SerializeField]
    private double xMin;

    [SerializeField]
    private double xMax;

    [SerializeField]
    private double yMin;

    [SerializeField]
    private double yMax;

    [SerializeField]
    private TMP_Text cityText;

    [SerializeField]
    private TMP_Text countryText;

    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private Button backButton;

    public static Action activateTravelPanel;

    private void ShowContries()
    {
        activateTravelPanel?.Invoke();
        countryParent.gameObject.SetActive(true);
        //cityContainer.gameObject.SetActive(false);


        if (countryParent.gameObject.GetComponentsInChildren<Button>().Length != GameDataManager.instance.GetCountryNames().Count)
        {
            //create the buttons
            foreach(string s in GameDataManager.instance.GetCountryNames())
            {
                GameObject g = Instantiate(buttonInstance, countryParent);
                g.GetComponentInChildren<TMP_Text>().text = s;

                g.GetComponent<Button>().onClick.AddListener(delegate { SpawnCitiesOld(s); });
            }
        }

    }

    private void SpawnCitiesOld(string name)
    {
        //activate cities panel
        countryParent.gameObject.SetActive(false);
        //cityContainer.gameObject.SetActive(true);
        

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
                    //AddListener(b, cities[i]);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void SpawnCities()
    {
        //activate cities panel
        activateTravelPanel?.Invoke();
        backButton.gameObject.SetActive(true);

        Button[] buttons = cityParent.GetComponentsInChildren<Button>();

        List<string> cities = GameDataManager.instance.GetCityNames();
        Debug.Log(cities);
        while (buttons.Length < cities.Count)
        {
            GameObject g = Instantiate(buttonInstance, cityParent);
            buttons = cityParent.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= cities.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < cities.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    Vector2 pos = LatLong2Canvas(new LatLng(GameDataManager.instance.GetCity(cities[i]).longitude, GameDataManager.instance.GetCity(cities[i]).latitude));
                    RectTransform rt = cityParent.GetComponent<RectTransform>();

                    Vector2 newPos = new Vector2((rt.rect.width * pos.x) - (rt.rect.width / 2), (rt.rect.height * pos.y) - (rt.rect.height / 2));
                    b.transform.localPosition = newPos;

                    AddListener(b, cities[i], GameDataManager.instance.GetCity(cities[i]).country);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private Vector2 LatLong2Canvas(LatLng latLng)
    {

        double rangeX = xMax - xMin;
        double rangeY = yMax - yMin;

        double correctedXValue = latLng.Latitude - xMin;
        double correctedYValue = latLng.Longitude - yMin;

        double xPercentage = (correctedXValue) / rangeX;
        double yPercentage = (correctedYValue) / rangeY;

        Vector2 rta = new Vector2((float)xPercentage, (float)yPercentage);

        return rta;
    }

    void AddListener(Button b, string value, string value2)
    {
        b.onClick.AddListener(() => gameStates.SelectPlace(value));
        b.onClick.AddListener(() => cityContainer.SetActive(false));
        b.onClick.AddListener(() => backButton.gameObject.SetActive(false));
        //b.onClick.AddListener(() => sitesTransform.gameObject.SetActive(false));
        b.GetComponent<ButtonHoverManager>().SetParameters(value, value2,cityText, countryText, timeText,cityContainer);
    }

    private void OnEnable()
    {
        GameStatesManager.showTravelUI += SpawnCities;
    }

    private void OnDisable()
    {
        GameStatesManager.showTravelUI -= SpawnCities;
    }
}
