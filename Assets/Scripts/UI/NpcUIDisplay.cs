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
    private Image placeImage;

    [SerializeField]
    private TMP_Text npcName;

    [SerializeField]
    private TMP_Text npcDialogue;

    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private TMP_Text placeText;

    [SerializeField]
    private TMP_Text cityText;

    [SerializeField]
    private TMP_Text countryText;    

    public static Action activateDialogue;

    private void SpawnNpcs(List<FriendlyNPC> npc)
    {

        //set the place sprite
        placeImage.sprite = IMG2Sprite.LoadNewSprite(GameDataManager.instance.placesPath + npc[0].GetPlace() + ".png");

        Button[] buttons = npcSpawner.GetComponentsInChildren<Button>();

        RectTransform rt = npcSpawner.GetComponent<RectTransform>();

        while (buttons.Length < npc.Count)
        {
            //instanciate the button
            GameObject g = Instantiate(npcPrefab, npcSpawner);
            buttons = npcSpawner.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= npc.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < npc.Count)
                {
            
                    Vector3 spawnPosition = GetBottomLeftCorner(rt) - new Vector3(UnityEngine.Random.Range(0, rt.rect.x), UnityEngine.Random.Range(0, rt.rect.y), 0);
                    buttons[i].gameObject.SetActive(true);
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectPlace(cities[i]);});
                    Button b = buttons[i];
                    b.transform.position = spawnPosition;
                    AddListener(b, npc[i].GetName());
                    b.GetComponent<Image>().sprite = IMG2Sprite.LoadNewSprite(GameDataManager.instance.npcPath + npc[i].GetGender().ToString() + "/" + npc[i].GetName() + ".png");
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

    }

    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    void AddListener(Button b, string value)
    {
        b.onClick.AddListener(() => gameStates.NpcSelected(value));
        //b.onClick.AddListener(() => sitesTransform.gameObject.SetActive(false));
    }

    private void SetTimeText(double time)
    {
        double minutes = time - Math.Truncate(time);

        double hours = time - minutes;

        minutes = (float)System.Math.Round(minutes, 2);

        minutes *= 60;

        minutes = Math.Floor(minutes);

        string s = hours + " h " + (minutes) + " min";

        timeText.text = s;
    }

    private void SetPlaceInformation(string place, string city, string country)
    {
        placeText.text = place;
        cityText.text = city;
        countryText.text = country;
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
        GameStatesManager.activateHUD?.Invoke();
    }

    private void OnEnable()
    {
        GameStatesManager.spawnNpc += SpawnNpcs;
        GameStatesManager.startDialogue += StartDialogue;
        GameStatesManager.setTimeText += SetTimeText;
        GameStatesManager.showPlaceInfo += SetPlaceInformation;
    }

    private void OnDisable()
    {
        GameStatesManager.spawnNpc -= SpawnNpcs;
        GameStatesManager.startDialogue -= StartDialogue;
        GameStatesManager.setTimeText -= SetTimeText;
        GameStatesManager.showPlaceInfo -= SetPlaceInformation;
    }
}
