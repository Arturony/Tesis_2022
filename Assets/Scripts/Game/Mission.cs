using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    private double maxTime;

    private float currenTime;

    private List<string> placesToGo;

    private Dictionary<string, List<FriendlyNPC>> npcs;

    private ArtPiece artPieceRobbed;

    private Robber robber;

    private string startingPlace;

    private string currentPlace;

    public Mission(double maxTime, ArtPiece artPieceRobbed, Robber robber, List<string> placesToGo, string startingPlace)
    {
        currenTime = 0f;
        this.placesToGo = placesToGo;
        npcs = new Dictionary<string, List<FriendlyNPC>>();
        this.artPieceRobbed = artPieceRobbed;
        this.robber = robber;
        this.startingPlace = startingPlace;
        this.currentPlace = startingPlace;
    }

    public void Travel(float travelTime)
    {
        currenTime += travelTime;
    }

    public float GetCurrenTime()
    {
        return currenTime;
    }

    public double GetMaxTime()
    {
        return maxTime;
    }

    public string GetCurrentPlace()
    {
        return currentPlace;
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

    public List<FriendlyNPC> GetNPCFromPlace(string place)
    {
        return npcs[place];
    }

    public Robber GetRobber()
    {
        return robber;
    }    

    public ArtPiece GetArtPieceRobbed()
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
