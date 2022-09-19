using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : IState
{
    private GameStatesManager manager;

    public Travel(GameStatesManager manager)
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
        //Get the location to travel. Remove travel time.
        //if time to travel is greater than time left, trigger game over
    }
}
