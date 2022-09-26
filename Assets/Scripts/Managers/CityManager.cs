using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField]
    private string citiesPath;

    private List<City> cities = new List<City>();

    private async Task LoadAllCitiesAsync()
    {

        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + citiesPath;
            string[] folder = Directory.GetFiles(path);
            IList<Task> writeTaskList = new List<Task>();
            foreach (string s in folder)
            {
                if (s.EndsWith(".json"))
                {
                    string text = await FileUtil.ReadAllTextAsync(s);
                    City c = JsonConvert.DeserializeObject<City>(text);
                    cities.Add(c);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void LoadAllCitiesEvent()
    {
        LoadAllCitiesAsync();
    }

    public City GetCity(string name)
    {

        foreach (City c in cities)
        {
            if (c.name.Equals(name))
            {
                return c;
            }
        }

        return null;
    }

    public List<string> GetCityNames()
    {
        List<string> rta = new List<string>();

        foreach (City c in cities)
        {
            rta.Add(c.name);
        }

        return rta;
    }

    private async Task SaveCitiesAsync()
    {
        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + citiesPath;
            List<City> tmp = cities;
            foreach (City q in tmp)
            {
                Debug.Log(q);
                string text = JsonConvert.SerializeObject(q);
                Debug.Log(text);
                using (var sw = new StreamWriter(path + q.name + ".json"))
                {
                    await sw.WriteAsync(text);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void OnEnable()
    {
        //LoaderManager.loadCountries += LoadAllCountriesEvent;
        //CountryDisplay.getCountryNames += GetCountryNames;
        List<string> tests = new List<string>();
        tests.Add("Test place 1");
        tests.Add("Test place 2");
        cities.Add(new City("Test city", "Test City descrption", tests, tests, 10.03723, -15.372513, "Test city id"));
        cities.Add(new City("Test city 2", "Test City 2 descrption", tests, tests, 99.03723, -55.372513, "Test city 2 id"));
    }

    private void OnDisable()
    {
        //LoaderManager.loadCountries -= LoadAllCountriesEvent;
        //CountryDisplay.getCountryNames -= GetCountryNames;
    }
}