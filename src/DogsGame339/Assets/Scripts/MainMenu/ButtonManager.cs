using System.Collections;
using Game.Runtime;
using Game339.Shared.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioManager AudioManager;
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
        StartCoroutine(StartButtonDelay());
    }

    public void OnTutorialButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Tutorial Button Pressed");
        StartCoroutine(TutorialButtonDelay());
    }
    
    public void OnExitButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Exit Button Pressed");
        StartCoroutine(ExitSceneButtonDelay());
    }
    
    public void OnTitleButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Title Button Pressed");
        AudioManager.PlayTitleButtonSound();
    }

    public void OnBackButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Back tutorial Button Pressed");
        StartCoroutine(MainMenuButtonDelay());

    }
    

    public void OnBackExitButtonPressed()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info("Back Exit Button Pressed");
        StartCoroutine(MainMenuButtonDelay());
        
    }

    IEnumerator MainMenuButtonDelay()
    {
        AudioManager.PlayOtherButtonSound();
        yield  return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
    
    
    IEnumerator ExitSceneButtonDelay()
    {
        AudioManager.PlayOtherButtonSound();
        yield  return new WaitForSeconds(1f);
        SceneManager.LoadScene("ExitScene");
    }

    IEnumerator StartButtonDelay()
    {
        AudioManager.PlayOtherButtonSound();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }
    
    IEnumerator TutorialButtonDelay()
    {
        AudioManager.PlayOtherButtonSound();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Tutorial");
    }
}
