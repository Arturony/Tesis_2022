using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatesManager : MonoBehaviour
{
    private StateMachine stateMachine;

    private bool isGamePaused = false;
    private bool gettingInfo = false;
    private bool showingInfo = false;
    private bool capturing = false;
    private bool traveling = false;
    private bool gameLost = false;
    private bool gameWon = false;

    public bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }
    public bool GettingInfo { get => gettingInfo; set => gettingInfo = value; }
    public bool ShowingInfo { get => showingInfo; set => showingInfo = value; }
    public bool Capturing { get => capturing; set => capturing = value; }
    public bool Traveling { get => traveling; set => traveling = value; }
    public bool GameLost { get => gameLost; set => gameLost = value; }
    public bool GameWon { get => gameWon; set => gameWon = value; }


    private MisionCreator missionCreator;

    private void Awake()
    {
        stateMachine = new StateMachine();

        missionCreator = gameObject.GetComponent<MisionCreator>();

        //states
        var gameStart = new GameStart(this, missionCreator);
        var travel = new Travel(this);
        var getInformation = new GetInformation(this);
        var showInformation = new ShowInformation(this);
        var capture = new Capture(this);
        var gameOver = new GameOver(this);
        var gameClear = new GameClear(this);
        var pause = new Pause(this);

        //transitions
        At(gameStart, getInformation, GettingInformation());

        At(pause, getInformation, GettingInformation());
        At(pause, showInformation, ShowingInformation());

        At(getInformation, showInformation, ShowingInformation());
        At(showInformation, getInformation, GettingInformation());

        At(getInformation, capture, ToCapture());

        At(getInformation, travel, ToTravel());
        At(travel, getInformation, GettingInformation());

        At(travel, gameOver, GameOverTransition());

        At(capture, gameOver, GameOverTransition());
        At(capture, gameClear, GameWonTransition());

        //initial state
        stateMachine.SetState(gameStart);

        //any transition
        stateMachine.AddAnyTransition(pause, GamePaused());

        void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);

        //transition functions
        Func<bool> GamePaused() => () => IsGamePaused == true;
        Func<bool> GettingInformation() => () => IsGamePaused == false && GettingInfo == true && ShowingInfo == false && Traveling == false && Capturing == false;
        Func<bool> ShowingInformation() => () => IsGamePaused == false && GettingInfo == false && ShowingInfo == true && Traveling == false && Capturing == false;
        Func<bool> ToCapture() => () => IsGamePaused == false && GettingInfo == false && ShowingInfo == false && Traveling == false && Capturing == true;
        Func<bool> ToTravel() => () => IsGamePaused == false && GettingInfo == false && ShowingInfo == false && Traveling == true && Capturing == false;
        Func<bool> GameOverTransition() => () => IsGamePaused == false && GettingInfo == false && ShowingInfo == false && Traveling == false && Capturing == false && GameLost == true && GameWon == false;
        Func<bool> GameWonTransition() => () => IsGamePaused == false && GettingInfo == false && ShowingInfo == false && Traveling == false && Capturing == false && GameLost == false && GameWon == true;

        
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
    }

}
