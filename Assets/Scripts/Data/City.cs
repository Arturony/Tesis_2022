using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City 
{
    public string name;
    public string description;
    public List<string> interestPlaces = new List<string>();
    public List<string> museums = new List<string>();
    public double latitude;
    public double longitude;
    public string id;

    public City(string name, string description, List<string> interestPlaces, List<string> museums, double latitude, double longitude, string id)
    {
        this.name = name;
        this.description = description;
        this.interestPlaces = interestPlaces;
        this.museums = museums;
        this.latitude = latitude;
        this.longitude = longitude;
        this.id = id;
    }
}
