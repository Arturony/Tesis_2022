using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : IState
{
    private GameStatesManager manager;

    public GameClear(GameStatesManager manager)
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
        //Show game clear screen. Set corresponding variables
    }
}
