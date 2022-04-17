using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeAndBackgroundMusic : MonoBehaviour
{
    //RUNTIME
    private AudioSource audioMusic;
    [SerializeField] private AudioClip retroSoundtrack;
    [SerializeField] private AudioClip rickLull;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip goodBossMusic;
    private int rand = 0;
    [SerializeField] private GameObject normalLights;
    [SerializeField] private GameObject purpleLights;
    [SerializeField] private GameObject normalCubeSkin;
    [SerializeField] private GameObject greyCubeSkin;
    private float musicVol = 1f;

    //FUNCTIONS
    void Start()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
        }

        audioMusic = GetComponent<AudioSource>();
        audioMusic.loop = true;

        rand = Random.Range(1,11);
        if(SceneManager.GetActiveScene().name == "LevelBoss"){
            rand = 11;
        } else if(SceneManager.GetActiveScene().name == "LevelBossGood"){
            rand = 12;
        }
        
        switch(rand){
            case 1:
                audioMusic.clip = rickLull;
                audioMusic.volume = 0.45f * musicVol;
                audioMusic.Play();
                normalLights.SetActive(false);
                purpleLights.SetActive(true);
                normalCubeSkin.SetActive(false);
                greyCubeSkin.SetActive(true);
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
                purpleLights.SetActive(false);
                normalLights.SetActive(true);
                greyCubeSkin.SetActive(false);
                normalCubeSkin.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if(GameManager.instance != null){
            musicVol = GameManager.instance.musicVol;
            if(rand == 1){
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
