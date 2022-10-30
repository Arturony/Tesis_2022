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

    [SerializeField]
    private TMP_Text timeText;

    public static Action activateDialogue;

    private void SpawnNpc(FriendlyNPC npc)
    {
        //instanciate the button
        GameObject g = Instantiate(npcPrefab, npcSpawner);
        //set the commad to execute
        g.GetComponent<Button>().onClick.AddListener(delegate { gameStates.NpcSelected(npc.GetName()); });

        //set the sprite

    }

    private void SetTimeText(double time)
    {
        double minutes = time - Math.Truncate(time);

        double hours = time - minutes;

        string s = hours + " h " + (minutes * 60) + " min";

        timeText.text = s;
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
        GameStatesManager.setTimeText += SetTimeText;
    }

    private void OnDisable()
    {
        GameStatesManager.spawnNpc -= SpawnNpc;
        GameStatesManager.startDialogue -= StartDialogue;
        GameStatesManager.setTimeText -= SetTimeText;
    }
}
