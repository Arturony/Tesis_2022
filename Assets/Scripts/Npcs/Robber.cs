using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber
{
    public string robberName;

    public List<string> tags;

    public string description;

    public Robber(string robberName, List<string> tags, string description)
    {
        this.robberName = robberName;
        this.tags = tags;
        this.description = description;
    }
}
