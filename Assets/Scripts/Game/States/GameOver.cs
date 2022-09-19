using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : IState
{
    private GameStatesManager manager;

    public GameOver(GameStatesManager manager)
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
        //Show game over screen. Set corresponding variables
    }
}
