using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource mainMenuBackgroundMusic;
    public AudioSource buttonSounds;


    public AudioClip mainMenuMusic;
    public AudioClip titleButtonSound;
    public AudioClip otherButtonSound;

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

    public void PlayOtherButtonSound()
    {
        buttonSounds.PlayOneShot(otherButtonSound);
    }

    public void PlayTitleButtonSound()
    {
        buttonSounds.PlayOneShot(titleButtonSound);
    }
    
    

}
