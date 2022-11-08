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
        GameStatesManager.showGameOver?.Invoke();
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        //Show game over screen. Set corresponding variables
    }
}
