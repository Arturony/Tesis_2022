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

    [SerializeField]
    private int minNumNpc;

    [SerializeField]
    private int maxNumNpc;

    private List<string> placesToTravel;

    private List<string> citiesPath;

    private List<FriendlyNPC> npcs;

    private Robber robber;

    private ArtPiece artPiece;

    private double time;

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

    public void MaxMissionTime()
    {
        time = 0;
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
    }

    public void PickArtPiece()
    {
        //pick first museum in places to travel and pick a random art piece

        Museum m = null;

        for (int i = 0; i < placesToTravel.Count; i++)
        {
            m = GameDataManager.instance.GetMuseum(placesToTravel[i]);

            if (m != null)
                break;
        }

        int rand = Random.Range(0, m.piecesId.Count);

        string artName = m.piecesId[rand];

        artPiece = GameDataManager.instance.GetArtPiece(artName);

    }

    public void PickRobber()
    {
        //pick random robber from list of robbers
        robber = GameDataManager.instance.GetRandomRobber();
    }

    public void SpawnNpcs()
    {
        //iterate through all the places. create between minNpcAmount and maxNpcAmmount.
        //pick random name and appearance. and random dialogue from the files

        List<List<string>> names = new List<List<string>>();
        npcs = new List<FriendlyNPC>();
        names.Add(GameDataManager.instance.GetNameByGender(FriendlyNPC.Gender.Male.ToString()));
        names.Add(GameDataManager.instance.GetNameByGender(FriendlyNPC.Gender.Female.ToString()));

        for(int i = 0; i < placesToTravel.Count; i++)
        {
            string nextPlace = "";

            if (i == placesToTravel.Count - 1)
            {
                nextPlace = placesToTravel[0];
            }
            else
            {
                nextPlace = placesToTravel[i + 1];
            }

            int numOfNpc = Random.Range(minNumNpc, maxNumNpc);

            for(int j = 0; j < numOfNpc; j++)
            {
                //get two random numbers to determine type and gender

                int randomType = Random.Range(0, 2);

                int randomGender = Random.Range(0, 2);

                int randomName = Random.Range(0, names[randomGender].Count);
                //get the attributes and create npc

                //npc type
                FriendlyNPC.NPCType npcType = randomType == 0 ? FriendlyNPC.NPCType.Helpful : FriendlyNPC.NPCType.Misc;
                //npc gender
                FriendlyNPC.Gender npcGender = randomGender == 0 ? FriendlyNPC.Gender.Male : FriendlyNPC.Gender.Female;
                //npc name
                string name = names[randomGender][randomName];

                FriendlyNPC n = new FriendlyNPC(npcType, npcGender, name, placesToTravel[i], nextPlace);
                npcs.Add(n);
            }
        }
    }

    public List<string> GetPathToFollow()
    {
        return placesToTravel;
    }    
}
