using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudioManager : MonoBehaviour
{
    //RUNTIME
    public static MenuAudioManager instance;
    private AudioSource audioMenuMusic;
    [SerializeField] private AudioClip mainMenuMusic;
    private bool replay = false;

    //FUNCTIONS
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
        Destroy(gameObject);
        }
    }

    void Start()
    {
        audioMenuMusic = GetComponent<AudioSource>();
        audioMenuMusic.clip = mainMenuMusic;
        audioMenuMusic.loop = true;
        audioMenuMusic.volume = 0.6f * GameManager.instance.musicVol;
        audioMenuMusic.Play();
    }

    void Update(){
        audioMenuMusic.volume = 0.6f * GameManager.instance.musicVol;

        if(!(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)){
            audioMenuMusic.Stop();
            replay = true;
        } else if(replay){
            audioMenuMusic.Play();
            replay = false;
        }
    }
}
