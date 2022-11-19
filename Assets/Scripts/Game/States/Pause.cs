using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : IState
{
    private GameStatesManager manager;

    private float pastTimeScale;

    public Pause(GameStatesManager manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {
        pastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Time.timeScale = pastTimeScale;
    }

    public void Tick()
    {
        //do pause stuff idk
    }
}
