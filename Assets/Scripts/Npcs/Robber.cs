using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber
{
    public string robberName;

    public List<string> tags;

    public Robber(string robberName, List<string> tags)
    {
        this.robberName = robberName;
        this.tags = tags;
    }
}
