using UnityEngine;

public class Gameer : MonoBehaviour
{
    //public UI UI;
    //public GameTimer GameTimer;
    //public BeerPlacer BeerPlacer;
    //public BonePlacer BonePlacer;
    //public PillPlacer PillPlacer;
    //public MoonshinePlacer MoonshinePlacer;
    //private PoopPlacer PoopPlacer;
    public Player Player;

    public bool IsPlaying = true; //CHANGE TO FALSE AFTER WE FIGURE OUT INITIALIZE
    private void Start()
    {
        //UI.ShowStartScreen();
    }

    private void Update()
    {
        //UI.ShowTime();
    }

    public void OnStartButtonClicked()
    {
        //UI.HideStartScreen();
        InitalizeGame();
    }

    private void InitalizeGame()
    {
        //GameTimer.StartTimer(GameParameters.GameDurationInSeconds, OnTimerFinished);
        //StartPlacers();
        IsPlaying = true;
        //ScoreKeeper.ResetScore();
        //UI.SetScoreText(0);
        Player.Reset();
    }

    public void OnPlayAgainButtonClicked()
    {
        //UI.HideGameOverScreen();
        InitalizeGame();
    }

    public void OnTimerFinished()
    {
        //UI.ShowEndScreen();
        //IsPlaying = false;
        //StopPlacers();
    }
}
