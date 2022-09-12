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

    public Museum(string name, string description, List<string> piecesId, string location, string id)
    {
        this.name = name;
        this.description = description;
        this.piecesId = piecesId;
        this.location = location;
        this.id = id;
    }
}
