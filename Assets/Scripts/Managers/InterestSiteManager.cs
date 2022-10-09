using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class InterestSiteManager : MonoBehaviour
{
    [SerializeField]
    private string citiesPath;

    private List<InterestSite> sites = new List<InterestSite>();

    private async Task LoadAllSitesAsync()
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
                    InterestSite c = JsonConvert.DeserializeObject<InterestSite>(text);
                    sites.Add(c);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public Task LoadAllSitesEvent()
    {
        return LoadAllSitesAsync();
    }

    public InterestSite GetSite(string name)
    {

        foreach (InterestSite c in sites)
        {
            if (c.name.Equals(name))
            {
                return c;
            }
        }

        return null;
    }

    public List<string> GetSiteNames()
    {
        List<string> rta = new List<string>();

        foreach (InterestSite c in sites)
        {
            rta.Add(c.name);
        }

        return rta;
    }


    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}
