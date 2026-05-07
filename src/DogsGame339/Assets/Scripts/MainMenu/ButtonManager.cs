using Game.Runtime;
using Game339.Shared.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameObject startButton;
    private GameObject tutorialButton;
    private GameObject exitButton;
    private GameObject titleButton;
    private GameObject backTutorialButton;
    private GameObject backExitButton;

    public void OnStartButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Start Button Pressed");
        //Debug.Log("Start Button Pressed");
        SceneManager.LoadScene("GameScene");
    }

    public void OnTutorialButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Tutorial Button Pressed");
        
        SceneManager.LoadScene("Tutorial");
        //Debug.Log("Tutorial Button Pressed");
    }
    
    public void OnExitButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Exit Button Pressed");
        
        SceneManager.LoadScene("ExitScene");
        
        //Debug.Log("Exit Button Pressed");
    }
    
    public void OnTitleButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Title Button Pressed");
        //Debug.Log("Title Button Pressed");
    }

    public void OnBackButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Back tutorial Button Pressed");
        SceneManager.LoadScene("MainMenu");
        
    }

    public void OnBackExitButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Back Exit Button Pressed");
        SceneManager.LoadScene("MainMenu");
    }

}
