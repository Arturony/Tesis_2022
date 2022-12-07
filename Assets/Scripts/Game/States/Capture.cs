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
        //show panels
        GameStatesManager.showRobberUI?.Invoke();
    }

    public void OnExit()
    {
        //hide panels
        RobberCaptureUIDisplay.activateRobberPanel?.Invoke();
    }

    public void Tick()
    {
        //Trigget capture secuence. Then show the result. 
        if(manager.RobberSelected == true)
        {
            manager.RobberSelected = false;
            manager.Capturing = false;

            if (GameDataManager.instance.GetCurrentMission().GetRobber().robberName.Equals(manager.GetRobberName()))
            {
                //win
                manager.GameWon = true;
                GameStatesManager.setWinText?.Invoke();
            }
            else
            {
                //lose
                manager.GameLost = true;
                GameStatesManager.setLoseByRobberText?.Invoke(manager.GetRobberName());
            }
        }
    }
}
