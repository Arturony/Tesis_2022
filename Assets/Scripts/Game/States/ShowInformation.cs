using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInformation : IState
{
    private GameStatesManager manager;

    public ShowInformation(GameStatesManager manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        //Show the menus that are in-game. Pause menu doesn't count here.
    }
}
