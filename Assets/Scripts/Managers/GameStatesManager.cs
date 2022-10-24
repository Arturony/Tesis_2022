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

    private bool npcSelected = false;

    public bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }
    public bool GettingInfo { get => gettingInfo; set => gettingInfo = value; }
    public bool ShowingInfo { get => showingInfo; set => showingInfo = value; }
    public bool Capturing { get => capturing; set => capturing = value; }
    public bool Traveling { get => traveling; set => traveling = value; }
    public bool GameLost { get => gameLost; set => gameLost = value; }
    public bool GameWon { get => gameWon; set => gameWon = value; }
    public bool NpcSelectedVar { get => npcSelected; set => npcSelected = value; }

    private MisionCreator missionCreator;

    private GameDataManager gameDataManager;

    //events

    public static Action<FriendlyNPC> spawnNpc;

    public static Action activateHUD;

    public static Action activateLoadingPanel;

    public static Action activatePlaceBackground;

    public static Action<string> setPlaceBackground;

    public static Action<string, string> startDialogue;

    //misc variables
    private string npcName;

    private void Awake()
    {
        stateMachine = new StateMachine();

        missionCreator = gameObject.GetComponent<MisionCreator>();
        gameDataManager = gameObject.GetComponent<GameDataManager>();

        //states
        var gameStart = new GameStart(this, missionCreator, gameDataManager);
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

    public void NpcSelected(string name)
    {
        npcSelected = true;
        npcName = name;
    }

    public string GetNpcSelected() => npcName;

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
    }

}
