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

    public FriendlyNPC(NPCType type, Gender gender, string name, List<string> dialogues, string place)
    {
        this.type = type;
        this.gender = gender;
        npcName = name;
        this.dialogues = dialogues;
        this.place = place;

        Dictionary<string, List<string>> commandValue = new Dictionary<string, List<string>>();

        List<string> robberValues = new List<string>();

        //get two random tags for the robber

        List<string> placeValues = new List<string>();

        foreach (string s in dialogues)
        {
            //replace the tags with real info
            TextHelper.ReplaceTags(s,GameDataManager.instance.GetStaringKey(), GameDataManager.instance.GetEndingKey(),commandValue);
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
        int indx = Random.Range(0, dialogues.Count);
        return dialogues[indx];
    }

    public NPCType GetNPCType()
    {
        return type;
    }
}
