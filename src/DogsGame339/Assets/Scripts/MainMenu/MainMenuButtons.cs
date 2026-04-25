using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private GameObject startButton;
    private GameObject tutorialButton;
    private GameObject exitButton;
    private GameObject titleButton;

    public void OnStartButtonPressed()
    {
        Debug.Log("Start Button Pressed");
    }

    public void OnTutorialButtonPressed()
    {
        Debug.Log("Tutorial Button Pressed");
    }
    
    public void OnExitButtonPressed()
    {
        Debug.Log("Exit Button Pressed");
    }
    
    public void OnTitleButtonPressed()
    {
        Debug.Log("Title Button Pressed");
    }
}
