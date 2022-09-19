using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNPC : MonoBehaviour
{
    private enum NPCType { Helpful, Misc };

    private string npcName;

    //this is to select the dialogue
    //Type:
    //Helpful: gives clues
    //Misc: gives facts of the location or random stuff about other characteristics
    private NPCType type;

    private List<string>[] dialogues;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
