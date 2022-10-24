using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NpcUIDisplay : MonoBehaviour
{
    [SerializeField]
    private GameStatesManager gameStates;

    [SerializeField]
    private GameObject npcPrefab;

    [SerializeField]
    private Transform npcSpawner;

    [SerializeField]
    private TMP_Text npcName;

    [SerializeField]
    private TMP_Text npcDialogue;

    public static Action activateDialogue;

    private void SpawnNpc(FriendlyNPC npc)
    {
        //instanciate the button
        GameObject g = Instantiate(npcPrefab, npcSpawner);
        //set the commad to execute
        g.GetComponent<Button>().onClick.AddListener(delegate { gameStates.NpcSelected(npc.GetName()); });

        //set the sprite

    }

    private void StartDialogue(string dialogue, string name)
    {
        GameStatesManager.activateHUD?.Invoke();
        activateDialogue?.Invoke();

        //temporal dialogue setting
        //add animation to the text and overflow control later
        npcName.text = name;

        npcDialogue.text = dialogue;
    }

    public void EndDialogue()
    {
        activateDialogue?.Invoke();
    }

    private void OnEnable()
    {
        GameStatesManager.spawnNpc += SpawnNpc;
        GameStatesManager.startDialogue += StartDialogue;
    }

    private void OnDisable()
    {
        GameStatesManager.spawnNpc -= SpawnNpc;
        GameStatesManager.startDialogue -= StartDialogue;
    }
}
