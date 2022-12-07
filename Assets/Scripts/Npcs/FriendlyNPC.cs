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

        //robber dialogues
        if (this.type == NPCType.Helpful)
        {
            List<string> robberValues = new List<string>(GameDataManager.instance.GetCurrentMission().GetRobber().tags);

            List<string> robberTags = new List<string>();

            int totalRobber = UnityEngine.Random.Range(1, total);

            total -= totalRobber;

            for (int i = 0; i < robberValues.Count && i < 4; i++)
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

            string placeToTell = nextPlace;

            List<string> cityDialogues = new List<string>();
            List<string> placeDialogues = new List<string>();
            List<string> piecesDialogues = new List<string>();

            //separate the dialogues
            foreach (string s in GameDataManager.instance.GetDialogueByTag(type.ToString()))
            {

                if (s.Contains("[" + GameDataManager.instance.GetCommands()[2] + "]"))
                {
                    //city command
                    cityDialogues.Add(s);
                }
                else if (s.Contains("[" + GameDataManager.instance.GetCommands()[1] + "]"))
                {
                    //place command
                    placeDialogues.Add(s);
                }
                else if (s.Contains("[" + GameDataManager.instance.GetCommands()[3] + "]"))
                {
                    //piece command
                    piecesDialogues.Add(s);
                }
            }


            //check if the place is a museum or a place. if is a museum get a random art piece and a dialog
            //do it only if it is a helpful npc
            List<string> places = new List<string>();
            Museum m = GameDataManager.instance.GetMuseum(placeToTell);
            if (m != null)
            {
                //get the dialogues for the art pieces
                places = new List<string>();
                places.Add(m.piecesId[UnityEngine.Random.Range(0, m.piecesId.Count)]);

                commandValue.Add(GameDataManager.instance.GetCommands()[3], places);

                List<string> dial = new List<string>(piecesDialogues);
                //List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[1].ToString()));

                int totalDialogue = UnityEngine.Random.Range(1, total);

                total -= totalDialogue;

                for (int i = 0; i < totalDialogue && i < dial.Count; i++)
                {
                    if (dial.Count > 0)
                    {
                        int temp = UnityEngine.Random.Range(0, dial.Count);
                        dialogues.Add(dial[temp]);
                        dial.RemoveAt(temp);
                    }
                }
            }

            //add the place only if is an interest place

            if (GameDataManager.instance.GetSite(placeToTell) != null)
            {
                places = new List<string>();
                places.Add(placeToTell);
                commandValue.Add(GameDataManager.instance.GetCommands()[1], places);

                List<string> dial = new List<string>(placeDialogues);
                //List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[1].ToString()));

                int totalDialogue = UnityEngine.Random.Range(1, total);

                total -= totalDialogue;

                for (int i = 0; i < totalDialogue && i < dial.Count; i++)
                {
                    if (dial.Count > 0)
                    {
                        int temp = UnityEngine.Random.Range(0, dial.Count);
                        dialogues.Add(dial[temp]);
                        dial.RemoveAt(temp);
                    }
                }
            }

            //get the city of the place. and the city of the current place

            places = new List<string>();

            string city = "";

            string currCity = "";

            try
            {
                if (GameDataManager.instance.GetSite(placeToTell) != null)
                    city = GameDataManager.instance.GetSite(placeToTell).city;
                else
                    city = GameDataManager.instance.GetMuseum(placeToTell).city;

                if (GameDataManager.instance.GetSite(place) != null)
                    currCity = GameDataManager.instance.GetSite(place).city;
                else
                    currCity = GameDataManager.instance.GetMuseum(place).city;
            }
            catch (Exception e)
            {
                Debug.Log(placeToTell);
                List<string> a = GameDataManager.instance.GetMuseumNames();
                foreach (string s in a)
                    Debug.Log(s);
            }

            //if the npc is helpful and is the same city, don't add it

            if (!city.Equals(currCity))
            {
                places.Add(city);

                commandValue.Add(GameDataManager.instance.GetCommands()[2], places);
                List<string> dial = new List<string>(cityDialogues);
                //List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[1].ToString()));
                int totalDialogue = UnityEngine.Random.Range(1, total);
                for (int i = 0; i < totalDialogue && i < dial.Count; i++)
                {
                    if (dial.Count > 0)
                    {
                        int temp = UnityEngine.Random.Range(0, dial.Count);
                        dialogues.Add(dial[temp]);
                        dial.RemoveAt(temp);
                    }
                }
            }
        }
        else
        {
            //add the place command
            List<string> places = new List<string>();

            places = new List<string>();
            places.Add(place);
            commandValue.Add(GameDataManager.instance.GetCommands()[1], places);

            //add the city command
            string city = "";

            if (GameDataManager.instance.GetSite(place) != null)
                city = GameDataManager.instance.GetSite(place).city;
            else
                city = GameDataManager.instance.GetMuseum(place).city;

            places.Add(city);

            commandValue.Add(GameDataManager.instance.GetCommands()[2], places);

            List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(type.ToString()));
            dial = new List<string>(GameDataManager.instance.GetDialogueByTag(type.ToString()));

            //List<string> dial = new List<string>(GameDataManager.instance.GetDialogueByTag(GameDataManager.instance.GetCommands()[1].ToString()));
            for (int i = 0; i < total && i < dial.Count; i++)
            {
                if (dial.Count > 0)
                {
                    int temp = UnityEngine.Random.Range(0, dial.Count);
                    dialogues.Add(dial[temp]);
                    dial.RemoveAt(temp);
                }
            }
        }



        //replace the commands with the actual values

        for(int i = 0; i < dialogues.Count; i++)
        {
            try
            {
                dialogues[i] = TextHelper.ReplaceTags(dialogues[i], GameDataManager.instance.GetStaringKey(), GameDataManager.instance.GetEndingKey(), commandValue);
            }
            catch
            {
                foreach(string s in dialogues)
                {
                    Debug.Log(s);
                }

                foreach(string s in commandValue.Keys)
                {
                    Debug.Log(s);
                }
                Debug.Log(type.ToString());
            }
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
