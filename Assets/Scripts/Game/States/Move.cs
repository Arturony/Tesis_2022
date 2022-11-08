using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    private GameStatesManager manager;

    public Move(GameStatesManager manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {
        //show panels
        Mission mission = GameDataManager.instance.GetCurrentMission();

        string name;

        if (GameDataManager.instance.GetSite(mission.GetCurrentPlace()) != null)
            name = GameDataManager.instance.GetSite(mission.GetCurrentPlace()).city;
        else
            name = GameDataManager.instance.GetMuseum(mission.GetCurrentPlace()).city; 

        GameStatesManager.showMoveUI?.Invoke(name);
    }

    public void OnExit()
    {
        MoveUIDisplay.activateSitesPanel?.Invoke();
    }

    public void Tick()
    {
        //Get the location to travel. Remove travel time.
        //if time to travel is greater than time left, trigger game over

        if (manager.PlaceSelected == true)
        {
            manager.PlaceSelected = false;
            Mission mission = GameDataManager.instance.GetCurrentMission();

            mission.SetCurrentPlace(manager.GetPlaceName());

            double time = 0.5;

            mission.Travel(time);

            if (mission.GetCurrenTime() > mission.GetMaxTime())
            {
                //trigger game over
                manager.Moving = false;
                manager.GameLost = true;
            }
            else
            {
                //continue

                //show animation or something
                manager.GettingInfo = true;
                manager.Moving = false;
            }

        }
    }
}
