using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class ArtPiecesManager : MonoBehaviour
{
    [SerializeField]
    private string museumsPath;

    private List<ArtPiece> artPieces = new List<ArtPiece>();

    private async Task LoadAllPiecesAsync()
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
                    ArtPiece c = JsonConvert.DeserializeObject<ArtPiece>(text);
                    artPieces.Add(c);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public Task LoadAllArtPiecesEvent()
    {
        return LoadAllPiecesAsync();
    }

    public ArtPiece GetArtPiece(string name)
    {

        foreach (ArtPiece c in artPieces)
        {
            if (c.title.Equals(name))
            {
                return c;
            }
        }

        return null;
    }

    public List<string> GetArtPiecesNames()
    {
        List<string> rta = new List<string>();

        foreach (ArtPiece c in artPieces)
        {
            rta.Add(c.title);
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
