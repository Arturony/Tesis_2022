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
        //activate journal panel
        GameStatesManager.showJournalPanel?.Invoke();
    }

    public void OnExit()
    {
        //deactivate journal panel
        GameStatesManager.showJournalPanel?.Invoke();
    }

    public void Tick()
    {
        //Show the menus that are in-game. Pause menu doesn't count here.
    }
}
