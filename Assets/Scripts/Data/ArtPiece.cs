using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece
{
    public string title;
    public string creator;
    public string date;
    public string description;
    public string origin;
    public string id;

    public ArtPiece(string title, string creator, string date, string description, string origin, string id)
    {
        this.title = title;
        this.creator = creator;
        this.date = date;
        this.description = description;
        this.origin = origin;
        this.id = id;
    }
}
