using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameStart : IState
{
    private GameStatesManager manager;

    private MisionCreator missionCreator;

    private GameDataManager gameDataManager;

    private List<Task> tasks;

    private bool assetsLoaded = false;

    public GameStart(GameStatesManager manager, MisionCreator missionCreator, GameDataManager gameDataManager)
    {
        this.manager = manager;
        this.missionCreator = missionCreator;
        this.gameDataManager = gameDataManager;
    }

    public void OnEnter()
    {
        tasks = gameDataManager.LoadAllAssets();
    }

    public void OnExit()
    {
        GameStatesManager.activateLoadingPanel?.Invoke();
    }

    public void Tick()
    {
        //first check if all resources have been loaded
        //Create random mission

        if(tasks != null && assetsLoaded == false)
        {
            int i = 0;
            foreach(Task t in tasks)
            {
                if (t.IsCompleted)
                    i++;
            }
            if (i == tasks.Count)
            {
                assetsLoaded = true;
                //all assets loaded

                //Spawn the player on the corresping location

                //To create a random mission:
                //Set a random path to follow
                //pick a random piece from the starting museum
                //calculate the time to travel to all the cities plus a small ammout
                //pick the robber
                //spawn the npc
                //pick 

                missionCreator.CreateMission();

                manager.GettingInfo = true;
            }
                
        }

    }
}

