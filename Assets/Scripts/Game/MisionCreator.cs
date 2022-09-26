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

    private List<string> placesToTravel;

    public void CreateMissionPath()
    {
        //first create the path to follow

        //path means where the clues will be distributed

        //how many countries we have to go to

        //create a copy of the countries list so we can remove the already selected countries

        List<string> contries = GameDataManager.instance.GetCountryNames();

        List<string> countriesPath = new List<string>();

        List<string> citiesPath = new List<string>();

        placesToTravel = new List<string>();

        int numberOfCountries = Random.Range(minCountryStops, contries.Count);

        for (int i = 0; i < numberOfCountries; i++)
        {
            int randomIndice = Random.Range(0, contries.Count);

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

    }

    public List<string> GetPathToFollow()
    {
        return placesToTravel;
    }    
}
