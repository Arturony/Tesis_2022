using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInformation : IState
{
    private GameStatesManager manager;

    private string place;

    private Mission mission;

    public GetInformation(GameStatesManager manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {
        mission = GameDataManager.instance.GetCurrentMission();

        place = mission.GetCurrentPlace();

        //The player is in a place. Show the place, menus and npcs. 

        //send the event to get the asset to show in the background
        GameStatesManager.activatePlaceBackground?.Invoke();
        GameStatesManager.setPlaceBackground?.Invoke(place);
        //send the event to activate the hud
        GameStatesManager.activateHUD?.Invoke();

        GameStatesManager.setTimeText?.Invoke(mission.GetMaxTime() - mission.GetCurrenTime());

        //spawn npcs
        foreach (FriendlyNPC n in mission.GetNPCFromPlace(place))
        {
            //send the event to spawn the button in the spawn area and set the sprite 
            GameStatesManager.spawnNpc?.Invoke(n);
        }

        if (GameDataManager.instance.GetSite(place) != null)
        {
            InterestSite s = GameDataManager.instance.GetSite(place);
            GameStatesManager.showPlaceInfo?.Invoke(place, s.city, s.country);
        } 
        else
        {
            Museum m = GameDataManager.instance.GetMuseum(place);
            GameStatesManager.showPlaceInfo?.Invoke(place, m.city, m.country);
        }
    }

    public void OnExit()
    {
        GameStatesManager.activateHUD?.Invoke();
        GameStatesManager.activatePlaceBackground?.Invoke();
    }

    public void Tick()
    {
        //wait to get an interaction with the npc
        
        if(manager.NpcSelectedVar)
        {
            string npcName = manager.GetNpcSelected();

            FriendlyNPC npc = GameDataManager.instance.GetCurrentMission().GetNPC(place, npcName);

            string dialogue = npc.GetRandomDialogue();

            //fire dialogue event

            GameStatesManager.startDialogue?.Invoke(dialogue, npcName);

            manager.NpcSelectedVar = false;
        }
    }
}