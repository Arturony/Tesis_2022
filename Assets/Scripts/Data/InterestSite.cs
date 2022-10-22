using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestSite
{
    public string name;
    public string description;
    public string location;
    public string id;
    public string country;
    public string city;
    public InterestSite(string name, string description, string location, string id, string country, string city)
    {
        this.name = name;
        this.description = description;
        this.location = location;
        this.id = id;
        this.country = country;
        this.city = city;
    }
}
