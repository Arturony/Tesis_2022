using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : IState
{
    private GameStatesManager manager;

    private MisionCreator missionCreator;

    public GameStart(GameStatesManager manager, MisionCreator missionCreator)
    {
        this.manager = manager;
        this.missionCreator = missionCreator;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        //Create random mission

        missionCreator.CreateMissionPath();

        //Spawn the player on the corresping location
        
        //To create a random mission:
        //Set the piece and the path the robber will follow. Create a robber.
        //Set the next museum and piece to rob.
        //Set the clues
        //Create and distribute the npc's
        //Spawn the player on the museum the piece belongs.



    }
}

