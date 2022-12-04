using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNPC
{
    public enum NPCType { Helpful, Misc };

    public enum Gender { Male, Female};

    private string npcName;    

    //this is to select the dialogue
    //Type:
    //Helpful: gives clues
    //Misc: gives facts of the location or random stuff about other characteristics
    private NPCType type;

    private Gender gender;

    private List<string> dialogues;

    private string place;

    private string nextPlace;

    public FriendlyNPC(NPCType type, Gender gender, string name, string place, string nextPlace)
    {
        this.type = type;
        this.gender = gender;
        npcName = name;
        this.dialogues = new List<string>();
        this.place = place;
        this.nextPlace = nextPlace;

        Dictionary<string, List<string>> commandValue = new Dictionary<string, List<string>>();

        //all the dialogues a npc can have

        int total = UnityEngine.Random.Range(2, 3);

        if (this.type == NPCType.Helpful)
        {
            List<string> robberValues = new List<string>(GameDataManager.instance.GetCurrentMission().GetRobber().tags);

            List<string> robberTags = new List<string>();

            int totalRobber = UnityEngine.Random.Range(1, total);

            total -= totalRobber;

            for (int i = 0; i < robberValues.Count || i < 4; i++)
            {
                if(robberValues.Count > 0)
                {
                    int temp = UnityEngine.Random.Range(0, robberValues.Count);
                    robberTags.Add(robberValues[temp]);
                    robberValues.RemoveAt(temp);
                }
            }

            //get four random tags for the robber
            List<string> diale = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[0].ToString()));
            for (int i = 0; i < totalRobber; i++)
            {
                if(diale.Count > 0)
                {
                    int temp = UnityEngine.Random.Range(0, diale.Count);
                    dialogues.Add(diale[temp]);
                    diale.RemoveAt(temp);
                }
            }

            commandValue.Add(GameDataManager.instance.GetCommands()[0], robberTags);
        }

        string placeToTell = type == NPCType.Helpful ? nextPlace: place;

        //get two random descriptions of the place according to the type of the npc
        List<string> places = new List<string>();
        places.Add(placeToTell);
        commandValue.Add(GameDataManager.instance.GetCommands()[1], places);

        List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(type.ToString()));
        //List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[1].ToString()));

        for (int i = 0; i < total || i < dial.Count; i++)
        {
            if(dial.Count > 0)
            {
                int temp = UnityEngine.Random.Range(0, dial.Count);
                dialogues.Add(dial[temp]);
                dial.RemoveAt(temp);
            }
        }

        //get two country descriptions according to the type of the npc
        places = new List<string>();

        string country = "";

        try
        {
            if (GameDataManager.instance.GetSite(placeToTell) != null)
                country = GameDataManager.instance.GetSite(placeToTell).city;
            else
                country = GameDataManager.instance.GetMuseum(placeToTell).city;
        }
        catch(Exception e)
        {
            Debug.Log(placeToTell);
            List<string> a = GameDataManager.instance.GetMuseumNames();
            foreach (string s in a)
                Debug.Log(s);
        }
        places.Add(country);

        commandValue.Add(GameDataManager.instance.GetCommands()[2], places);


        /*dial = new List<string>(GameDataManager.instance.GetDialogueByTag(type.ToString()));
        //dial = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[2].ToString()));

        int totalCity = UnityEngine.Random.Range(0, total);

        total -= totalPlaces;

        for (int i = 0; i < 2 || i < dial.Count; i++)
        {
            if (dial.Count > 0)
            {
                int temp = UnityEngine.Random.Range(0, dial.Count);
                dialogues.Add(dial[temp]);
                dial.RemoveAt(temp);
            }
        }*/

        for(int i = 0; i < dialogues.Count; i++)
        {
            dialogues[i] = TextHelper.ReplaceTags(dialogues[i], GameDataManager.instance.GetStaringKey(), GameDataManager.instance.GetEndingKey(), commandValue);
        }
    }

    public Gender GetGender()
    {
        return gender;
    }

    public string GetName()
    {
        return npcName;
    }

    public string GetRandomDialogue()
    {
        int indx = UnityEngine.Random.Range(0, dialogues.Count);
        return dialogues[indx];
    }

    public NPCType GetNPCType()
    {
        return type;
    }

    public string GetPlace()
    {
        return place;
    }
}
