using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource mainMenuBackgroundMusic;


    public AudioClip mainMenuMusic;
    public AudioClip startButtonSound;
    public AudioClip tutorialButtonSound;
    public AudioClip exitButtonSound;
    public AudioClip titleButtonSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMainMenuBackgroundMusic(mainMenuMusic);
    }

private void PlayMainMenuBackgroundMusic(AudioClip clip)
    {
        mainMenuBackgroundMusic.PlayOneShot(clip);
    }
    
}
