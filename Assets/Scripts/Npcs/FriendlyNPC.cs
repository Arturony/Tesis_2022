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

        if(this.type == NPCType.Helpful)
        {
            List<string> robberValues = new List<string>();

            //get four random tags for the robber

            commandValue.Add(GameDataManager.instance.GetCommands()[0], robberValues);
        }

        string placeToTell = type == NPCType.Helpful ? nextPlace: place; 

        //get two random descriptions of the place according to the type of the npc

        commandValue.Add(GameDataManager.instance.GetCommands()[1], GameDataManager.instance.GetDialogueByTag(type.ToString()+placeToTell));

        //get two country descriptions according to the type of the npc

        commandValue.Add(GameDataManager.instance.GetCommands()[2], GameDataManager.instance.GetDialogueByTag(type.ToString() + GameDataManager.instance.GetSite(placeToTell).country));


        foreach(List<string> list in commandValue.Values)
        {
            foreach(string s in list)
            {
                dialogues.Add(TextHelper.ReplaceTags(s, GameDataManager.instance.GetStaringKey(), GameDataManager.instance.GetEndingKey(), commandValue));
            }
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
