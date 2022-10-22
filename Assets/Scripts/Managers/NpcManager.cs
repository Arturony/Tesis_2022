using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    //this class loads the robbers, npc names and dialogues for them

    [SerializeField]
    private string dialoguesPath;

    [SerializeField]
    private string robbersPath;

    [SerializeField]
    private string namesPath;

    private Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();

    private List<Robber> robbers = new List<Robber>();

    private Dictionary<string, List<string>> dialogues = new Dictionary<string, List<string>>();

    //load names from .json

    private async Task LoadNamesAsync()
    {

        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + namesPath;
            string[] folder = Directory.GetFiles(path);
            IList<Task> writeTaskList = new List<Task>();
            foreach (string s in folder)
            {
                if (s.EndsWith(".json"))
                {
                    string text = await FileUtil.ReadAllTextAsync(s);
                    names = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(text);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    //load robbers from .json

    private async Task LoadRobbersAsync()
    {

        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + robbersPath;
            string[] folder = Directory.GetFiles(path);
            IList<Task> writeTaskList = new List<Task>();
            foreach (string s in folder)
            {
                if (s.EndsWith(".json"))
                {
                    string text = await FileUtil.ReadAllTextAsync(s);
                    robbers = JsonConvert.DeserializeObject<List<Robber>>(text);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    //load dialogues from .json

    private async Task LoadDialoguesAsync()
    {

        try
        {
            string path = System.IO.Directory.GetCurrentDirectory() + dialoguesPath;
            string[] folder = Directory.GetFiles(path);
            IList<Task> writeTaskList = new List<Task>();
            foreach (string s in folder)
            {
                if (s.EndsWith(".json"))
                {
                    string text = await FileUtil.ReadAllTextAsync(s);
                    dialogues = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(text);
                }
            }
            await Task.WhenAll(writeTaskList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public Task LoadAllDialoguesEvent()
    {
        return LoadDialoguesAsync();
    }

    public Task LoadAllNamesEvent()
    {
        return LoadNamesAsync();
    }

    public Task LoadAllRobbersEvent()
    {
        return LoadRobbersAsync();
    }

    public List<string> GetDialogueByTag(string tag)
    {
        return dialogues[tag];
    }

    public List<string> GetNameByGender(string tag)
    {
        return names[tag];
    }

    public Robber GetRandomRobber()
    {
        int rand = UnityEngine.Random.Range(0, robbers.Count);

        return robbers[rand];
    }
}
