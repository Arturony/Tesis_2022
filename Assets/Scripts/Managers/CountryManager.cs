using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class CountryManager : MonoBehaviour
{
    [SerializeField]
    private string countriesPath;

    private List<Country> countries = new List<Country>();

    private async Task LoadAllCountriesAsync()
    {
        
        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + countriesPath;
            string[] folder = Directory.GetFiles(path);
            IList<Task> writeTaskList = new List<Task>();
            foreach (string s in folder)
            {
                if (s.EndsWith(".json"))
                {
                    string text = await FileUtil.ReadAllTextAsync(s);
                    Country c = JsonConvert.DeserializeObject<Country>(text);
                    countries.Add(c);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public Task LoadAllCountriesEvent()
    {
        return LoadAllCountriesAsync();
    }

    public Country GetCountry(string name)
    {

        foreach(Country c in countries)
        {
            if(c.name.Equals(name))
            {
                return c;
            }
        }

        return null;
    }

    public List<string> GetCountryNames()
    {
        List<string> rta = new List<string>();

        foreach (Country c in countries)
        {
            rta.Add(c.name);
        }

        return rta;
    }

    private async Task SaveCountriesAsync()
    {
        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + countriesPath;
            List<Country> tmp = countries;
            foreach (Country q in tmp)
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
        
    }

    private void OnDisable()
    {
        
    }
}
