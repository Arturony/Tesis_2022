using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class MuseumManager : MonoBehaviour
{
    [SerializeField]
    private string museumsPath;

    private List<Museum> museums = new List<Museum>();

    private async Task LoadAllMuseumsAsync()
    {

        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + museumsPath;
            string[] folder = Directory.GetFiles(path);
            IList<Task> writeTaskList = new List<Task>();
            foreach (string s in folder)
            {
                if (s.EndsWith(".json"))
                {
                    string text = await FileUtil.ReadAllTextAsync(s);
                    Museum c = JsonConvert.DeserializeObject<Museum>(text);
                    museums.Add(c);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public Task LoadAllMuseumsEvent()
    {
        return LoadAllMuseumsAsync();
    }

    public Museum GetMuseum(string name)
    {

        foreach (Museum c in museums)
        {
            if (c.name.Equals(name))
            {
                return c;
            }
        }

        return null;
    }

    public List<string> GetMuseumNames()
    {
        List<string> rta = new List<string>();

        foreach (Museum c in museums)
        {
            rta.Add(c.name);
        }

        return rta;
    }

    private void OnEnable()
    {
        //LoaderManager.loadCountries += LoadAllCountriesEvent;
        //CountryDisplay.getCountryNames += GetCountryNames;
    }

    private void OnDisable()
    {
        //LoaderManager.loadCountries -= LoadAllCountriesEvent;
        //CountryDisplay.getCountryNames -= GetCountryNames;
    }

}