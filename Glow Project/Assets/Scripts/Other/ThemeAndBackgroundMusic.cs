using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeAndBackgroundMusic : MonoBehaviour
{
    //RUNTIME
    private AudioSource audioMusic;
    [SerializeField] private AudioClip tutorialMusic;
    [SerializeField] private AudioClip retroSoundtrack;
    [SerializeField] private AudioClip rickLull;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip goodBossMusic;
    private int rand = 0;
    private float musicVol = 1f;

    //GETTER y SETTER
    public int GetTABMrand(){
        return rand;
    }

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
        }

        audioMusic = GetComponent<AudioSource>();
        audioMusic.loop = true;

        rand = Random.Range(1,11);
        if(SceneManager.GetActiveScene().name == "LevelTutorial"){
            rand = 0;
        }
        if(SceneManager.GetActiveScene().name == "LevelBoss"){
            rand = 11;
        } else if(SceneManager.GetActiveScene().name == "LevelBossGood"){
            rand = 12;
        }
        
        switch(rand){
            case 0:
                audioMusic.clip = tutorialMusic;
                audioMusic.volume = 0.5f * musicVol;
                audioMusic.Play();
                break;
            case 1:
                audioMusic.clip = rickLull;
                audioMusic.volume = 0.45f * musicVol;
                audioMusic.Play();
                break;
            case 11:
                audioMusic.clip = bossMusic;
                audioMusic.volume = 1f * musicVol;
                audioMusic.Play();
                break;
            case 12:
                audioMusic.clip = goodBossMusic;
                audioMusic.volume = 0.5f * musicVol;
                audioMusic.Play();
                break;
            default:
                audioMusic.clip = retroSoundtrack;
                audioMusic.volume = 0.3f * musicVol;
                audioMusic.Play();
                break;
        }
    }

    void Update()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
            if(rand == 0){
                audioMusic.volume = 0.5f * musicVol;
            } else if(rand == 1){
                audioMusic.volume = 0.45f * musicVol;
            } else if(rand == 11){
                audioMusic.volume = 1f * musicVol;
            } else if(rand == 12){
                audioMusic.volume = 0.5f * musicVol;
            } else{
                audioMusic.volume = 0.3f * musicVol;
            }
        }
    }
}
