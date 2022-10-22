using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum 
{
    public string name;
    public string description;
    public List<string> piecesId = new List<string>();
    public string location;
    public string id;
    public string country;
    public string city;
    public Museum(string name, string description, List<string> piecesId, string location, string id, string country, string city)
    {
        this.name = name;
        this.description = description;
        this.piecesId = piecesId;
        this.location = location;
        this.id = id;
        this.country = country;
        this.city = city;
    }
}
