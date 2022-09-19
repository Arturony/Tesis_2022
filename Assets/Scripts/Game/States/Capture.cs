using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : IState
{
    private GameStatesManager manager;

    public Capture(GameStatesManager manager)
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
        //Trigget capture secuence. Then show the result. 
    }
}
