using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [SerializeField]
    private CountryManager countryManager;
    [SerializeField]
    private CityManager cityManager;
    [SerializeField]
    private MuseumManager museumManager;
    [SerializeField]
    private ArtPiecesManager artPiecesManager;

    private bool hasLoaded = false;

    public List<Task> LoadAllAssets()
    {
        if (hasLoaded == false)
        {
            hasLoaded = true;
            List<Task> tasks = new List<Task>();

            tasks.Add(countryManager.LoadAllCountriesEvent());
            tasks.Add(cityManager.LoadAllCitiesEvent());
            //tasks.Add(museumManager.LoadAllMuseumsEvent());
            //tasks.Add(artPiecesManager.LoadAllArtPiecesEvent());

            return tasks;
        }
        else
            return null;
    }

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

    public List<string> GetArtPiecesNames()
    {
        return artPiecesManager.GetArtPiecesNames();
    }

    public ArtPiece GetArtPiece(string name)
    {
        return artPiecesManager.GetArtPiece(name);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
