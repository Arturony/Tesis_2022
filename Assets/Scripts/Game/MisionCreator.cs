using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionCreator : MonoBehaviour
{
    [SerializeField]
    private int maxCityStops;

    [SerializeField]
    private int maxMuseumStops;

    [SerializeField]
    private int maxPlacesStops;

    [SerializeField]
    private int minPlacesStops;

    [SerializeField]
    private int minCountryStops;

    [SerializeField]
    private float additionalTime;

    [SerializeField]
    private float flightSpeed;

    private List<string> placesToTravel;

    private List<string> citiesPath;

    public void CreateMissionPath()
    {
        //first create the path to follow

        //path means where the clues will be distributed

        //how many countries we have to go to

        //create a copy of the countries list so we can remove the already selected countries

        List<string> contries = GameDataManager.instance.GetCountryNames();

        List<string> countriesPath = new List<string>();

        citiesPath = new List<string>();

        placesToTravel = new List<string>();

        int numberOfCountries = Random.Range(minCountryStops, contries.Count);

        for (int i = 0; i < numberOfCountries; i++)
        {
            int randomIndice = Random.Range(0, contries.Count-1);

            string name = contries[randomIndice];

            //remove it so we are sure we don't choose it again
            contries.Remove(name);

            //add it to the countries to travel to
            countriesPath.Add(name);

            Country c = GameDataManager.instance.GetCountry(name);

            List<string> cities = c.cities;

            int numberCities = Random.Range(1, maxCityStops);
            //check the country cities and select some
            for (int j = 0; j < numberCities; j++)
            {
                randomIndice = Random.Range(0, cities.Count);

                name = cities[randomIndice];

                cities.Remove(name);

                citiesPath.Add(name);

                City city = GameDataManager.instance.GetCity(name);

                int numberMuseums = Random.Range(1, maxMuseumStops);
                int numberPlaces = Random.Range(minPlacesStops, maxPlacesStops);

                List<string> museums = city.museums;
                List<string> places = city.interestPlaces;

                for (int z = 0; z < numberMuseums; z++)
                {
                    randomIndice = Random.Range(0, museums.Count);

                    name = museums[randomIndice];

                    museums.Remove(name);

                    placesToTravel.Add(name);
                }

                for (int w = 0; w < numberPlaces; w++)
                {
                    randomIndice = Random.Range(0, places.Count);

                    name = places[randomIndice];

                    places.Remove(name);

                    placesToTravel.Add(name);
                }
            }
        }
        foreach (string s in placesToTravel)
            Debug.Log(s);
    }

    public double MaxMissionTime()
    {
        double time = 0;
        double dist = 0;
        LatLng prev = null;

        foreach(string s in citiesPath)
        {
            City c = GameDataManager.instance.GetCity(s);
            //get the city and calculate the distance between the first and the second
            if (prev == null)
            {
                prev = new LatLng(c.latitude, c.longitude);
            }
            else
            {
                LatLng act = new LatLng(c.latitude, c.longitude);
                dist += HarvesineDistance.HaversineDistance(prev, act);
                prev = act;
            }
        }

        time = dist / flightSpeed;

        time += additionalTime;

        return time;
    }



    public List<string> GetPathToFollow()
    {
        return placesToTravel;
    }    
}
