using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    private float maxTime;

    private float currenTime;

    private List<string> placesToGo;

    private Dictionary<string, List<FriendlyNPC>> npcs;

    private string artPieceRobbed;

    private string robber;

    private string startingPlace;

    public Mission(float maxTime, string artPieceRobbed, string robber, List<string> placesToGo, string startingPlace)
    {
        currenTime = 0f;
        this.placesToGo = placesToGo;
        npcs = new Dictionary<string, List<FriendlyNPC>>();
        this.artPieceRobbed = artPieceRobbed;
        this.robber = robber;
        this.startingPlace = startingPlace;
    }

    public void Travel(float travelTime)
    {
        currenTime += travelTime;
    }

    public float GetCurrenTime()
    {
        return currenTime;
    }

    public float GetMaxTime()
    {
        return maxTime;
    }

    public void AddNpc(string place, FriendlyNPC npc)
    {
        if(npcs.ContainsKey(place))
        {
            npcs[place].Add(npc);
        }
        else
        {
            List<FriendlyNPC> temp = new List<FriendlyNPC>();
            temp.Add(npc);
            npcs.Add(place, temp);
        }
    }

    public FriendlyNPC GetNPC(string place, string name)
    {
        List<FriendlyNPC> temp = npcs[place];
        foreach(FriendlyNPC f in temp)
        {
            if (f.GetName().Equals(name))
                return f;
        }
        return null;
    }

    public string GetRobber()
    {
        return robber;
    }    

    public string GetArtPieceRobbed()
    {
        return artPieceRobbed;
    }

    public List<string> GetPlacesToGo()
    {
        return placesToGo;
    }

    public string GetStartingPlace()
    {
        return startingPlace;
    }
}
