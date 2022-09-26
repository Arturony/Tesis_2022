using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    #region Singleton
    public static GameDataManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private CountryManager countryManager;

    private CityManager cityManager;

    private MuseumManager museumManager;

    private ArtPiecesManager artPiecesManager;


    public List<string> GetCountryNames()
    {
        return countryManager.GetCountryNames();
    }

    public Country GetCountry(string name)
    {
        return countryManager.GetCountry(name);
    }

    public List<string> GetCityNames()
    {
        return cityManager.GetCityNames();
    }

    public City GetCity(string name)
    {
        return cityManager.GetCity(name);
    }

    public List<string> GetMuseumNames()
    {
        return museumManager.GetMuseumNames();
    }

    public Museum GetMuseum(string name)
    {
        return museumManager.GetMuseum(name);
    }

    private void OnEnable()
    {
        countryManager = gameObject.GetComponent<CountryManager>();
        cityManager = gameObject.GetComponent<CityManager>();
        museumManager = gameObject.GetComponent<MuseumManager>();
        artPiecesManager = gameObject.GetComponent<ArtPiecesManager>();
    }

    private void OnDisable()
    {
        
    }
}
