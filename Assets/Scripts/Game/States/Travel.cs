using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : IState
{
    private GameStatesManager manager;

    private double flightSpeed;

    public Travel(GameStatesManager manager, double flightSpeed)
    {
        this.manager = manager;
        this.flightSpeed = flightSpeed;
    }

    public void OnEnter()
    {
        //show travel panels
        GameStatesManager.showTravelUI?.Invoke();
    }

    public void OnExit()
    {
        //disable panels
        TravelUIDisplay.activateTravelPanel?.Invoke();
    }

    public void Tick()
    {
        //Get the location to travel. Remove travel time.
        //if time to travel is greater than time left, trigger game over

        if (manager.PlaceSelected == true)
        {
            manager.PlaceSelected = false;
            Mission mission = GameDataManager.instance.GetCurrentMission();

            string currentPlace = mission.GetCurrentPlace();

            foreach(string s in GameDataManager.instance.GetCity(manager.GetPlaceName()).interestPlaces)
            {
                InterestSite site = GameDataManager.instance.GetSite(s);

                if(site.type.Equals("Aeropuerto"))
                {
                    mission.SetCurrentPlace(site.name);

                    City from;

                    if(GameDataManager.instance.GetSite(currentPlace) != null)
                        from = GameDataManager.instance.GetCity(GameDataManager.instance.GetSite(currentPlace).city);
                    else
                        from = GameDataManager.instance.GetCity(GameDataManager.instance.GetMuseum(currentPlace).city);

                    City to = GameDataManager.instance.GetCity(site.city);

                    double dist = HarvesineDistance.HaversineDistance(new LatLng(from.latitude, from.longitude), new LatLng(to.latitude, to.longitude));

                    double time = dist / flightSpeed;

                    mission.Travel(time);

                    if(mission.GetCurrenTime() > mission.GetMaxTime())
                    {
                        //trigger game over
                        manager.GameLost = true;
                        manager.Traveling = false;
                    }
                    else
                    {
                        //continue

                        //show animation or something
                        manager.GettingInfo = true;
                        manager.Traveling = false;
                    }

                }    
            }
            
        }
    }
}
