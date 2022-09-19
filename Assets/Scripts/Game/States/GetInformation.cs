using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInformation : IState
{
    private GameStatesManager manager;

    public GetInformation(GameStatesManager manager)
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
        //Get the object or npc that the player is interacting with. Show interaction.
    }
}