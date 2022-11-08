using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobberCaptureUIDisplay : MonoBehaviour
{
    [SerializeField]
    private GameStatesManager gameStates;

    [SerializeField]
    private GameObject buttonInstance;

    [SerializeField]
    private Transform robbersTransform;

    public static Action activateRobberPanel;

    private void SpawnRobbers()
    {
        //activate cities panel
        activateRobberPanel?.Invoke();

        Button[] buttons = robbersTransform.GetComponentsInChildren<Button>();

        List<string> robs = GameDataManager.instance.GetAllRobbers();

        while (buttons.Length < robs.Count)
        {
            GameObject g = Instantiate(buttonInstance, robbersTransform);
            buttons = robbersTransform.GetComponentsInChildren<Button>();
        }

        if (buttons.Length >= robs.Count)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < robs.Count)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = robs[i];
                    //buttons[i].onClick.AddListener(delegate { gameStates.SelectRobber(robs[i]);});
                    Button b = buttons[i];
                    AddListener(b, robs[i]);

                    // set sprite
                }
                else if (i >= robs.Count)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void AddListener(Button b, string value)
    {
        b.onClick.AddListener(() => gameStates.SelectRobber(value));
        //b.onClick.AddListener(() => sitesTransform.gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        GameStatesManager.showRobberUI += SpawnRobbers;
    }

    private void OnDisable()
    {
        GameStatesManager.showRobberUI -= SpawnRobbers;
    }
}
