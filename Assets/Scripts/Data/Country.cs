using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country 
{
    public string name;
    public string description;
    public List<string> cities = new List<string>();

    public Country(string name, string description, List<string> cities)
    {
        this.name = name;
        this.description = description;
        this.cities = cities;
    }
}
