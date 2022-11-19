using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    private bool paused = false;

    private float pastScale;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameStatesManager statesManager;

    private void Start()
    {
        pastScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == true)
                UnPause();
            else
                Pauses();
        }
    }

    public void Pauses()
    {
        pastScale = Time.timeScale;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        paused = true;
        statesManager.IsGamePaused = true;
    }

    public void UnPause()
    {
        Time.timeScale = pastScale;
        pausePanel.SetActive(false);
        paused = false;
        statesManager.IsGamePaused = false;
    }

    public void UnPauseExit()
    {
        Time.timeScale = pastScale;
        pausePanel.SetActive(false);
        paused = false;
        statesManager.IsGamePaused = false;
    }
}
